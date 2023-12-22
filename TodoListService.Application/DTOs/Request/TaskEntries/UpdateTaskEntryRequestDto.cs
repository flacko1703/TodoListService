namespace TodoListService.Application.DTOs.Request.Note;

public record UpdateNoteRequestDto(Guid TodoListId, 
    Guid NoteId,
    string Title, 
    string Text, 
    List<string> Tags, 
    bool IsDone);