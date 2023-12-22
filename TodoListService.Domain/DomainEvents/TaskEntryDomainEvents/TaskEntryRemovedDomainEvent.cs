using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.DomainEvents;

public record TaskEntryRemovedDomainEvent(Guid Id, Guid NoteId) : DomainEvent(Id)
{
    
}