using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;
using TodoListService.Domain.Enum;

namespace TodoListService.Application.DTOs.Response;

//Generate DTOs for note responses
public record TaskEntryResponseDto(Guid Id, 
    string Title, 
    string? Text, 
    List<TagResponseDto>? Tags, 
    string Status);