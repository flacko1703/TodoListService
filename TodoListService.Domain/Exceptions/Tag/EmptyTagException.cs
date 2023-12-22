using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.Exceptions.Tag;

public class EmptyTagException : DomainException
{
    public EmptyTagException() 
        : base("Tag cannot be empty")
    {
    }
}