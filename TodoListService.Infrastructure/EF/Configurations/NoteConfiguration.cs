using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoListService.Domain.Aggregates.TodoListAggregate.Entities;
using TodoListService.Domain.Enum;

namespace TodoListService.Infrastructure.EF.Configurations;

public class NoteConfiguration : IEntityTypeConfiguration<TaskEntry>
{
    public void Configure(EntityTypeBuilder<TaskEntry> builder)
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
        
        //Text Configuration
        builder.Property(c => c.Text)
            .HasConversion(x => x.Value, 
                x => x)
            .IsRequired(false)
            .HasColumnOrder(3);
        
        //IsDone Configuration
        builder.Property(c => c.Status)
            .HasConversion(
                v => v.ToString(),
                v => (Status)Enum.Parse(typeof(Status), v))
            .HasColumnOrder(4);
        
        //Tags Configuration
        builder.HasMany(x => x.Tags)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}