using FluentResults;
using TodoListService.Application.DTOs.Response;
using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;
using TodoListService.Domain.Enum;

namespace TodoListService.Application.Services;

public interface ITodoListOperationService
{
    Task<Result<IEnumerable<TaskEntryResponseDto>>> FilterTaskEntriesByTagAsync(TagId tagId, 
        CancellationToken cancellationToken);
    Task<Result<IEnumerable<TaskEntryResponseDto>>> FilterTaskEntriesByStatusAsync(Status status, 
        CancellationToken cancellationToken);
    
    Task<Result<IEnumerable<TaskEntryResponseDto>>> ChangeTaskEntryStatus(TaskEntryId id, 
        Status status, 
        CancellationToken cancellationToken);
    
    Task<Result<IEnumerable<TaskEntryResponseDto>>> MoveTaskEntryToAnotherTodolist(TodoListId id, 
        TaskEntryId taskEntryId, 
        CancellationToken cancellationToken);
    
}