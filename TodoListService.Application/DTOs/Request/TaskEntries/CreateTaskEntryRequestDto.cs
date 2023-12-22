namespace TodoListService.Application.DTOs.Request.Note;

public record CreateNoteRequestDto(string Title, string Text, List<string> Tags, bool IsDone);