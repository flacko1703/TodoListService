using TodoListService.Application.DTOs.Request.Note;

namespace TodoListService.Application.DTOs.Request.TodoList;

public record UpdateTodoListRequestDto(Guid Id, 
    string Title, 
    List<UpdateNoteRequestDto> Notes);