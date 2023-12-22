using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

namespace TodoListService.Application.DTOs.Request.Tags;

public record UpdateTagRequestDto(TagId TagId, TagName TagName);