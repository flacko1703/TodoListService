namespace TodoListService.Domain.SeedWork;

public abstract record AuditableEntity : Entity, IAuditableEntity
{
    public DateTime CreatedAtUtc { get; init; }
    public DateTime? UpdatedAtUtc { get; init; }
    
    public uint Version { get; init; }
}