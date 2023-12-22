using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.DomainEvents.TaskEntryDomainEvents;

public record TaskEntryUpdatedDomainEvent(Guid Id) : DomainEvent(Id)
{
    
}