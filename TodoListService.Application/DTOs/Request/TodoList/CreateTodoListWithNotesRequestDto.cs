using TodoListService.Application.DTOs.Request.Note;

namespace TodoListService.Application.DTOs.Request.TodoList;

public record CreateTodoListWithNotesRequestDto(string Title, List<CreateNoteRequestDto> Notes);