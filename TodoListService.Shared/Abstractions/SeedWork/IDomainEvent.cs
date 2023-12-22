using MediatR;

namespace TodoListService.Domain.SeedWork;

/// <summary>
/// Marker interface for domain events.
/// </summary>
public interface IDomainEvent : INotification
{
    Guid Id { get; }
}