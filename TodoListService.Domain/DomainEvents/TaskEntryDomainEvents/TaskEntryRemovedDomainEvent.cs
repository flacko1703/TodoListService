using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.DomainEvents.TaskEntryDomainEvents;

public record TaskEntryRemovedDomainEvent(Guid Id) : DomainEvent(Id)
{
    
}