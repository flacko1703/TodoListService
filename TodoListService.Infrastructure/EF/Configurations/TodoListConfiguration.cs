using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoListService.Domain.Aggregates.TodoListAggregate;

namespace TodoListService.Infrastructure.EF.Configurations;

public class TodoListConfiguration : IEntityTypeConfiguration<TodoList>
{
    public void Configure(EntityTypeBuilder<TodoList> builder)
    {

        //Id Configuration
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnOrder(1);

        //Title Configuration
        builder.Property(c => c.Title)
            .HasConversion(x => x.Value, 
                x => x)
            .IsRequired()
            .HasColumnOrder(2);
        
        //Notes Configuration
        builder.HasMany(x => x.TaskEntries)
            .WithOne();
        
        //Added Configuration
        builder.Property(c => c.CreatedAtUtc)
            .HasColumnName("Created")
            .HasColumnOrder(3);
        
        //Updated Configuration
        builder.Property(c => c.UpdatedAtUtc)   
            .HasColumnName("Updated")
            .HasColumnOrder(4);

        builder.Property(p => p.Version)
            .IsConcurrencyToken()
            .IsRowVersion()
            .HasColumnName("xmin")
            .HasColumnOrder(5);
        
        builder.HasMany(x => x.TaskEntries)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

    }
}