using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoListProj.Domain.Aggregates.TodoListAggregate.Entities;

namespace TodoListService.Infrastructure.EF.Configurations;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
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
        
        //Text Configuration
        builder.Property(c => c.Text)
            .HasConversion(x => x.Value, 
                x => x)
            .IsRequired(false)
            .HasColumnOrder(3);
        
        //IsDone Configuration
        builder.Property(c => c.IsDone)
            .HasColumnOrder(4);
        
        //Tags Configuration
        builder.HasMany(x => x.Tags)
            .WithOne();
        
    }
}