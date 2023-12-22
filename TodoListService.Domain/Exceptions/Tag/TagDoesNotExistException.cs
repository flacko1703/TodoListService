using TodoListService.Domain.SeedWork;

namespace TodoListService.Domain.Exceptions.Tag;

public class TagDoesNotExistException : DomainException
{
    public TagDoesNotExistException() 
        : base($"Tag does not exist.")
    {
    }
}