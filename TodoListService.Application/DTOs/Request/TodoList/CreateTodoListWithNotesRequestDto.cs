using TodoListService.Application.DTOs.Request.TaskEntries;
using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

namespace TodoListService.Application.DTOs.Request.TodoList;

public record CreateTodoListWithNotesRequestDto(TodoListTitle Title, List<CreateTaskEntryRequestDto> Notes);