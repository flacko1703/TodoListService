namespace TodoListService.Application.Abstractions;

public interface IUnitOfWork
{
    Task TrackAuditableEntitiesAsync(CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}