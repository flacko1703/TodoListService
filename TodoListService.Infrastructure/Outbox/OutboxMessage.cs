namespace TodoListService.Infrastructure.Outbox;

public sealed class OutboxMessage
{
    public Guid Id { get; set; }
    
    public string Type { get; set; } = null!;
    
    public string Content { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? SentAt { get; set; }
    
    public string Error { get; set; }
}