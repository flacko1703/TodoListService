namespace TodoListService.Shared.Messaging.Contracts;

public record TodoListDeleted
{
    public Guid Id { get; init; }
}