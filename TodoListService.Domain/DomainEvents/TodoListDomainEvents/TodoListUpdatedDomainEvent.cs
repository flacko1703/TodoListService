using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.DomainEvents.TodoListDomainEvents;

public record TodoListUpdatedDomainEvent(Guid Id) : DomainEvent(Id)
{
    
}