using FluentResults;
using Mapster;
using MediatR;
using TodoListService.Domain.Repositories;
using TodoListService.Application.DTOs.Response;

namespace TodoListService.Application.UseCases.TodoLists.Queries.GetNotesByTodoListId;

public class GetNotesByTodoListIdQueryHandler : IRequestHandler<GetNotesByTodoListIdQuery, Result<IEnumerable<TaskEntryResponseDto>>>
{
    private readonly ITodoListRepository _todoListRepository;

    public GetNotesByTodoListIdQueryHandler(ITodoListRepository todoListRepository)
    {
        _todoListRepository = todoListRepository;
    }

    public async Task<Result<IEnumerable<TaskEntryResponseDto>>> Handle(GetNotesByTodoListIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _todoListRepository.GetByIdAsync(request.TodoListId, cancellationToken);
        
        if (result is null)
        {
            return Result.Fail(new Error($"Cannot find todolist with Id {request.TodoListId}"));
        }
        
        return result.TaskEntries.Adapt<List<TaskEntryResponseDto>>();
    }
}