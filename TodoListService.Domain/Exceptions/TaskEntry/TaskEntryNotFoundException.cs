using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.Exceptions.TaskEntry;

public class TaskEntryNotFoundException : DomainException
{
    public TaskEntryNotFoundException(Guid? id) 
        : base($"TaskEntry with {id} does not exist.")
    {
    }
}