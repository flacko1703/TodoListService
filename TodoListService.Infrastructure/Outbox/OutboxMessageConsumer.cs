namespace TodoListService.Infrastructure.Outbox;

public sealed class OutboxMessageConsumer
{
    public Guid Id { get; init; }
    
    public string Name { get; init; }
}