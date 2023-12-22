using TodoListService.Domain.Enum;

namespace TodoListService.Application.DTOs.Request.TaskEntries;

public record UpdateTaskEntryRequestDto(Guid TodoListId, 
    Guid TaskEntryId,
    string Title, 
    string Text, 
    List<string> Tags, 
    Status Status);