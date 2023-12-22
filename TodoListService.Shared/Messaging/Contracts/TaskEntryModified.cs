namespace TodoListService.Shared.Messaging.Contracts;

public record TaskEntryModified
{
    public Guid Id { get; init; }
    
    public Guid TodoListId { get; init; }
}