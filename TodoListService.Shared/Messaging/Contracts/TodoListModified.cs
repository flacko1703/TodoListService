namespace TodoListService.Shared.Messaging.Contracts;

public record TodoListModified
{
    public Guid Id { get; init; }
    
    public string Title { get; init; }
}