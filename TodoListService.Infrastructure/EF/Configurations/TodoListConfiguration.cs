using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoListProj.Domain.Aggregates.TodoListAggregate;

namespace TodoListService.Infrastructure.EF.Configurations;

public class TodoListConfiguration : IEntityTypeConfiguration<TodoList>
{
    public void Configure(EntityTypeBuilder<TodoList> builder)
    {

        //Id Configuration
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(
                v => v.Value,
                v => v)
            .IsRequired()
            .HasColumnOrder(1);

        //Title Configuration
        builder.Property(c => c.Title)
            .HasConversion(x => x.Value, 
                x => x)
            .IsRequired()
            .HasColumnOrder(2);
        
        //Notes Configuration
        builder.HasMany(x => x.Notes)
            .WithOne();
        
        //Added Configuration
        builder.Property(c => c.CreatedAtUtc)
            .HasColumnName("Created")
            .HasColumnOrder(3);
        
        //Updated Configuration
        builder.Property(c => c.UpdatedAtUtc)
            .HasColumnName("Updated")
            .HasColumnOrder(4);
        
        
    }
}