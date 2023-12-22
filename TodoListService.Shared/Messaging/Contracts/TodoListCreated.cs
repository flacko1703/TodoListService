namespace TodoListService.Shared.Messaging.Contracts;

public record TodoListCreatedEvent
{
    public Guid Id { get; init; }
    
    public string Title { get; init; }
}