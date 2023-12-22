namespace TodoListService.Shared.Messaging.Contracts;

public record TaskEntryDeleted
{
    public Guid Id { get; init; }
    
    public Guid TodoListId { get; init; }
    
}