namespace TodoListService.Application.DTOs.Response;

//Generate DTOs for note responses
public record NoteResponseDto(Guid Id, 
    string Title, 
    string? Text, 
    List<TagResponseDto>? Tags, 
    bool? IsDone);