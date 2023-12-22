namespace TodoListService.Shared.Messaging.Contracts;

public record TodoListCreated
{
    public Guid Id { get; init; }
    
    public string Title { get; init; }
}