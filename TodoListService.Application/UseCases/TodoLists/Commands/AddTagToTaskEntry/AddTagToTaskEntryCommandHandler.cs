using FluentResults;
using MediatR;
using TodoListService.Application.DTOs.Response;
using TodoListService.Domain.Aggregates.TodoListAggregate.Entities;
using TodoListService.Domain.Repositories;
using TodoListService.Shared.Abstractions;

namespace TodoListService.Application.UseCases.TodoLists.Commands.AddTagToTaskEntry;

public class AddTagToTaskEntryCommandHandler : IRequestHandler<AddTagToTaskEntryCommand, Result<TaskEntryResponseDto>>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public AddTagToTaskEntryCommandHandler(ITodoListRepository todoListRepository, IUnitOfWork unitOfWork)
    {
        _todoListRepository = todoListRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TaskEntryResponseDto>> Handle(AddTagToTaskEntryCommand request, CancellationToken cancellationToken)
    {
        var todoList = await _todoListRepository.GetByIdAsync(request.TodoListId, cancellationToken);
        
        if (todoList is null)
        {
            return Result.Fail<TaskEntryResponseDto>("Todolist not found");
        }
        
        var taskEntry = todoList.TaskEntries?.FirstOrDefault(x => x.Id == request.TaskEntryId.Value);
        
        if (taskEntry is null)
        {
            return Result.Fail<TaskEntryResponseDto>("Note not found");
        }
        
        var tag = Tag.Create(request.Tag.Name);
        
        taskEntry.AddTag(tag);
        
        await _todoListRepository.UpdateAsync(todoList, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Ok();
    }
}