using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.Exceptions.TaskEntry;

public class EmptyTaskEntryTextException : DomainException
{
    public EmptyTaskEntryTextException() 
        : base($"TaskEntry text is empty.")
    {
        
    }
}