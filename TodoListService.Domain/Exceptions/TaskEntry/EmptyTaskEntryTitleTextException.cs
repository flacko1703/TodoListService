using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.Exceptions.TaskEntry;

public class EmptyTaskEntryTitleTextException : DomainException
{
    public EmptyTaskEntryTitleTextException() 
        : base($"TaskEntry Title text cannot be empty.")
    {
        
    }
}