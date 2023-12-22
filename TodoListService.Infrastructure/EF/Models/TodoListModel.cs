namespace TodoListService.Infrastructure.EF.Models;

public record TodoListModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<Guid>? Notes { get; set; }
    
    public DateTime CreatedAtUtc { get; set; }
    
    public DateTime? UpdatedAtUtc { get; set; }
}