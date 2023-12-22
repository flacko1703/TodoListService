namespace TodoListService.Shared.Abstractions.SeedWork;

public class DomainException : Exception
{
    protected DomainException(string? message) : base(message)
    {
        
    }
}