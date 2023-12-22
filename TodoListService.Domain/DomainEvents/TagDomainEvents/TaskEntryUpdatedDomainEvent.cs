using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.DomainEvents;

public record TaskEntryUpdatedDomainEvent(Guid Id) : DomainEvent(Id)
{
    
}