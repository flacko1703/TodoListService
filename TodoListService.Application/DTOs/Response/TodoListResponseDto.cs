using TodoListService.Application.DTOs.Request.Notes;
using TodoListProj.Domain.Aggregates.TodoListAggregate.Entities;
using TodoListProj.Domain.Aggregates.TodoListAggregate.ValueObjects;

namespace TodoListService.Application.DTOs.Response;

public record TodoListResponseDto(Guid Id, string Title, List<NoteResponseDto>? Notes);