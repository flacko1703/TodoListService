using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.DomainEvents.TodoListDomainEvents;

public record TodoListRemovedDomainEvent(Guid Id) : DomainEvent(Id)
{
    
}