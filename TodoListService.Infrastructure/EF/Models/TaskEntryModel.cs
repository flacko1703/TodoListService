namespace TodoListService.Infrastructure.EF.Models;

public record NoteModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Text { get; set; } = string.Empty;
    public List<TagModel>? Tags { get; set; } = new();
    public bool IsDone { get; set; }
    public Guid TodoListModelId { get; set; } 
}