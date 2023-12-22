using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.Exceptions.TaskEntry;

public class EmptyTaskEntryIdException : DomainException
{
    public EmptyTaskEntryIdException() 
        : base($"TaskEntry id cannot be empty.")
    {
    }
}