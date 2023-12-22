using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.DomainEvents;

public record TodoListCreatedDomainEvent(Guid Id) : DomainEvent(Id)
{
    
}