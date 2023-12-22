namespace TodoListService.Domain.SeedWork;

public class DomainException : Exception
{
    protected DomainException(string? message) : base(message)
    {
        
    }
}