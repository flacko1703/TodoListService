using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.DomainEvents.TaskEntryDomainEvents;

public sealed record TaskEntryCreatedDomainEvent(Guid Id) : DomainEvent(Id)
{
    
}