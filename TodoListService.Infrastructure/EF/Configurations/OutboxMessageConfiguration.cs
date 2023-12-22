using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoListService.Infrastructure.Outbox;

namespace TodoListService.Infrastructure.EF.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired();
        
        builder.Property(x => x.Type)
            .HasColumnName("type")
            .IsRequired();
        
        builder.Property(x => x.Content)
            .HasColumnName("content")
            .IsRequired();
        
        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();
        
        builder.Property(x => x.SentAt)
            .HasColumnName("sent_at");
        
        builder.Property(x => x.Error)
            .HasColumnName("error")
            .IsRequired();
    }
}