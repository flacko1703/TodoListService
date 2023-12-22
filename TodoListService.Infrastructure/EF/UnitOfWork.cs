using Microsoft.EntityFrameworkCore;
using TodoListService.Infrastructure.EF.Contexts;
using TodoListService.Shared.Abstractions;
using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Infrastructure.EF;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task TrackAuditableEntitiesAsync(CancellationToken cancellationToken = default)
    {
        var entries = _dbContext
            .ChangeTracker
            .Entries<IAuditableEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(x => x.CreatedAtUtc).CurrentValue = DateTime.UtcNow;
            }
            
            if (entry.State == EntityState.Modified)
            {
                entry.Property(x => x.UpdatedAtUtc).CurrentValue = DateTime.UtcNow;
            }
        }
        
        await Task.CompletedTask;
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await TrackAuditableEntitiesAsync(cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}