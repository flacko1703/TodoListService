using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.DomainEvents.TagDomainEvents;

public record TagRemovedDomainEvent(Guid Id) : DomainEvent(Id)
{
    
}