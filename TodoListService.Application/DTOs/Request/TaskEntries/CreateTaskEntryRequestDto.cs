namespace TodoListService.Application.DTOs.Request.TaskEntries;

public record CreateTaskEntryRequestDto(string Title, string Text, List<string> Tags);