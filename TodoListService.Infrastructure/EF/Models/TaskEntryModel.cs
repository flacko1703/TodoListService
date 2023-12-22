using TodoListService.Domain.Enum;

namespace TodoListService.Infrastructure.EF.Models;

public record TaskEntryModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Text { get; set; } = string.Empty;
    public List<TagModel>? Tags { get; set; } = new();
    public string Status { get; set; } = string.Empty;
    public Guid TodoListModelId { get; set; } 
}