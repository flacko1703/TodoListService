namespace TodoListService.Infrastructure.EF.Models;

public record TagModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid NoteModelId { get; set; } 
}