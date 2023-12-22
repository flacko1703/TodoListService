using Microsoft.EntityFrameworkCore;
using TodoListService.Application.Abstractions;
using TodoListProj.Domain.SeedWork;
using TodoListService.Infrastructure.EF.Contexts;

namespace TodoListService.Infrastructure.EF;

public class UnitOfWork : IUnitOfWork
{
    private readonly TodoListDbContext _context;

    public UnitOfWork(TodoListDbContext context)
    {
        _context = context;
    }
    
    public async Task TrackAuditableEntitiesAsync(CancellationToken cancellationToken = default)
    {
        var entries = _context
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
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}