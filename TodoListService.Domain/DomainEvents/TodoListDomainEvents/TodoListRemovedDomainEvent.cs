using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.DomainEvents;

public record TodoListDeletedDomainEvent(Guid Id) : DomainEvent(Id)
{
    
}