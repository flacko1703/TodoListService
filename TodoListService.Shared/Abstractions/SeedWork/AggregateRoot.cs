namespace TodoListService.Domain.SeedWork;

/// <summary>
/// Base record for Entities which are aggregates
/// </summary>
public abstract record AggregateRoot : AuditableEntity, IDomainEvent
{
    private List<IDomainEvent> _domainEvents = new();
    
    public IEnumerable<IDomainEvent> DomainEvents => _domainEvents.ToList().AsReadOnly();

    protected void AddDomainEvent(IDomainEvent eventItem)
    {
       _domainEvents.Add(eventItem);
    }
    
    protected void RemoveDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents.Remove(eventItem);
    }

    public IEnumerable<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList().AsReadOnly();
    }

    public void ClearEvents()
    {
        _domainEvents.Clear();
    }
}