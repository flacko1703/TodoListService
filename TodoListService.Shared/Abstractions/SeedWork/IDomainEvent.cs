using MediatR;

namespace TodoListService.Shared.Abstractions.SeedWork;

/// <summary>
/// Marker interface for domain events.
/// </summary>
public interface IDomainEvent : INotification
{
    Guid Id { get; }
}