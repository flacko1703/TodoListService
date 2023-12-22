using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;
using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.DomainEvents;

public sealed record TaskEntryCreatedDomainEvent(Guid Id) : DomainEvent(Id)
{
    
}