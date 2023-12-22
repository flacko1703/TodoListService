using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.DomainEvents.TodoListDomainEvents;

public record TodoListCreatedDomainEvent(Guid Id) : DomainEvent(Id)
{
    
}