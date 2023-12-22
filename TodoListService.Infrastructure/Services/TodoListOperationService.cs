using FluentResults;
using Mapster;
using TodoListService.Application.DTOs.Response;
using TodoListService.Application.Services;
using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;
using TodoListService.Domain.Enum;
using TodoListService.Domain.Repositories;
using TodoListService.Shared.Abstractions;

namespace TodoListService.Infrastructure.Services;

public class TodoListOperationService : ITodoListOperationService
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    
    public TodoListOperationService(ITodoListRepository todoListRepository, IUnitOfWork unitOfWork)
    {
        _todoListRepository = todoListRepository;
        _unitOfWork = unitOfWork;
    }

    
    public async Task<Result<IEnumerable<TaskEntryResponseDto>>> FilterTaskEntriesByTagAsync(TagId tagId, 
        CancellationToken cancellationToken)
    {
        var todoLists = await _todoListRepository.GetAllAsync(cancellationToken);
        
        if (todoLists is null)
        {
            return Result.Fail("Todo lists not found");
        }
        
        var notes = todoLists
            .SelectMany(x => x.TaskEntries)
            .Where(x => x.Tags.All(x => x.Id == tagId.Value));
        
        return Result.Ok(notes.Select(x => x.Adapt<TaskEntryResponseDto>()));
    }

    public async Task<Result<IEnumerable<TaskEntryResponseDto>>> FilterTaskEntriesByStatusAsync(Status status, 
        CancellationToken cancellationToken)
    {
        var todoLists = await _todoListRepository.GetAllAsync(cancellationToken);
        
        if (todoLists is null)
        {
            return Result.Fail("Todolists not found");
        }
        
        var taskEntries = todoLists
            .SelectMany(x => x.TaskEntries)
            .Where(x => x.Status == status);
        
        return Result.Ok(taskEntries.Select(x => x.Adapt<TaskEntryResponseDto>()));
    }

    public Task<Result<IEnumerable<TaskEntryResponseDto>>> ChangeTaskEntryStatus(TaskEntryId id, Status status, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<TaskEntryResponseDto>>> MoveTaskEntryToAnotherTodolist(TodoListId id, 
        TaskEntryId taskEntryId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}