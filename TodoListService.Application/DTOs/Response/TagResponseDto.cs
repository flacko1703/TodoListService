using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

namespace TodoListService.Application.DTOs.Response;

public record TagResponseDto(Guid Id, string Name);