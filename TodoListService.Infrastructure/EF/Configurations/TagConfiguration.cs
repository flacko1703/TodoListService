using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoListProj.Domain.Aggregates.TodoListAggregate.Entities;

namespace TodoListService.Infrastructure.EF.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {

        //Id Configuration
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(
                v => v.Value,
                v => v)
            .IsRequired()
            .HasColumnOrder(1);

        //Name Configuration
        builder.Property(c => c.TagName)
            .HasConversion(x => x.Value, 
                x => x)
            .IsRequired()
            .HasColumnOrder(2);
    }
}