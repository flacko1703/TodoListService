namespace TodoListService.Application.DTOs.Response;

public record TodoListResponseDto(Guid Id, string Title, List<TaskEntryResponseDto>? Notes);