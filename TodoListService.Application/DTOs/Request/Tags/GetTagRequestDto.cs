using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

namespace TodoListService.Application.DTOs.Request.Tags;

public record GetTagRequestDto(TagId TagId);