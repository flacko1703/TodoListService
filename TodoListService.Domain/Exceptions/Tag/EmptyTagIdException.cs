using TodoListService.Domain.SeedWork;

namespace TodoListService.Domain.Exceptions.Tag;

public class EmptyTagIdException : DomainException
{
    public EmptyTagIdException() 
        : base($"Tag id cannot be empty.")
    {
    }
}