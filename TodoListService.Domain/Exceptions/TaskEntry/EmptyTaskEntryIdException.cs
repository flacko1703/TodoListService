using TodoListService.Domain.SeedWork;

namespace TodoListService.Domain.Exceptions.Tag;

public class EmptyTaskEntryIdException : DomainException
{
    public EmptyTaskEntryIdException() 
        : base($"TaskEntry id cannot be empty.")
    {
    }
}