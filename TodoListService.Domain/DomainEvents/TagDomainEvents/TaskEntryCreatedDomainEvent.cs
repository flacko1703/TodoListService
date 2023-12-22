using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.DomainEvents.TagDomainEvents;

public sealed record TagCreatedDomainEvent(Guid Id) : DomainEvent(Id)
{
    
}