using FluentResults;
using Mapster;
using MediatR;
using TodoListService.Application.Abstractions;
using TodoListService.Application.DTOs.Response;
using TodoListService.Domain.Aggregates.TodoListAggregate.Entities;
using TodoListService.Domain.Repositories;

namespace TodoListService.Application.UseCases.TodoLists.Commands.CreateTaskEntry;

public class CreateNoteCommandHandler : IRequestHandler<CreateTaskEntryCommand, Result<TaskEntryResponseDto>>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateNoteCommandHandler(ITodoListRepository todoListRepository, IUnitOfWork unitOfWork)
    {
        _todoListRepository = todoListRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<TaskEntryResponseDto>> Handle(CreateTaskEntryCommand request, CancellationToken cancellationToken)
    {
        var todoList = await _todoListRepository.GetByIdAsync(request.TodoListId, cancellationToken);
        
        if (todoList is null)
        {
            return Result.Fail<TaskEntryResponseDto>("Todolist not found");
        }
        
        var note = TaskEntry.Create(request.TaskEntryRequestDto.Title, 
            request.TaskEntryRequestDto.Text,
            request.TaskEntryRequestDto.Tags.Select(x => Tag.Create(x)).ToList());
        
        todoList.AddTaskEntry(note);
        
        await _todoListRepository.UpdateAsync(todoList, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = note.Adapt<TaskEntryResponseDto>();
        
        return result;
    }
}