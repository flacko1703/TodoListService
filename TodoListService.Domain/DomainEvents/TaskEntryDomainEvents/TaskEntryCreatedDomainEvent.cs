using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.DomainEvents;

public sealed record TaskEntryCreatedDomainEvent(Guid Id, Guid NoteId) : DomainEvent(Id)
{
    
}