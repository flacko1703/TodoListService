using TodoListService.Application.DTOs.Request.TaskEntries;
using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

namespace TodoListService.Application.DTOs.Request.TodoList;

public record UpdateTodoListRequestDto(TodoListId Id, 
    TodoListTitle Title, 
    List<UpdateTaskEntryRequestDto> Notes);