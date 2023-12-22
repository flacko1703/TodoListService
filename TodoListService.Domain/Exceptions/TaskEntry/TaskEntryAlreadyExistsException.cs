using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;
using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.Exceptions.TaskEntry;

public class TaskEntryAlreadyExistsException : DomainException
{
    public TaskEntryAlreadyExistsException(TaskEntryId taskEntryId) 
        : base($"TaskEntry with id: {taskEntryId} already exists.")
    {
        
    }
}