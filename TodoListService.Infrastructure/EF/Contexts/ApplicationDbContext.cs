using Microsoft.EntityFrameworkCore;
using TodoListService.Domain.Aggregates.TodoListAggregate;
using TodoListService.Infrastructure.Outbox;

namespace TodoListService.Infrastructure.EF.Contexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }
    
    public DbSet<TodoList> TodoLists { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<OutboxMessageConsumer> OutboxMessageConsumers { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InfrastructureAssemblyReference).Assembly);
    }
    
}