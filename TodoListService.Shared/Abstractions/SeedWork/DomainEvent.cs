namespace TodoListService.Shared.Abstractions.SeedWork;

public abstract record DomainEvent(Guid Id) : IDomainEvent
{
    
}