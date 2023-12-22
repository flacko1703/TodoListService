using FluentResults;
using MediatR;
using TodoListService.Application.DTOs.Response;
using TodoListService.Domain.Repositories;

namespace TodoListService.Application.UseCases.TodoLists.Queries.GetNoteById;

public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, Result<TaskEntryResponseDto>>
{
    private readonly ITodoListRepository _todoListRepository;
    
    public GetNoteByIdQueryHandler(ITodoListRepository todoListRepository)
    {
        _todoListRepository = todoListRepository;
    }


    public async Task<Result<TaskEntryResponseDto>> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        var todoList = await _todoListRepository.GetByIdAsync(request.TodoListId, cancellationToken);
        
        if (todoList is null)
        {
            return Result.Fail("Note not found");
        }
        
        var note = todoList.TaskEntries.FirstOrDefault(x => x.Id == request.NoteId);
        
        if (note is null)
        {
            return Result.Fail("Note not found");
        }
        
        var result = new TaskEntryResponseDto(note.Id,
            note.Title,
            note.Text,
            note.Tags?.Select(x => new TagResponseDto(x.Id,x.Name)).ToList(),
            note.Status.ToString());

        return result;
    }
}