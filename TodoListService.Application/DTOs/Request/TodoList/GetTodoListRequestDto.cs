using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

namespace TodoListService.Application.DTOs.Request.TodoList;

public record GetTodoListRequestDto(TodoListId Id);