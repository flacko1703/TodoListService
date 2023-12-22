using FluentResults;
using TodoListService.Application.DTOs.Request.Note;
using TodoListService.Application.DTOs.Request.Tag;
using TodoListService.Application.DTOs.Request.TodoList;
using TodoListService.Application.DTOs.Response;
using TodoListService.Domain.Aggregates.TodoListAggregate;

namespace TodoListService.Application.Services;

public interface ITodoListService
{
    Task<Result<TodoList>> AddNoteAsync(Guid todoListId, CreateNoteRequestDto noteRequestDto, CancellationToken cancellationToken);
    Task<Result<IEnumerable<NoteResponseDto>>> FilterNotesByTagAsync(Guid tagId, CancellationToken cancellationToken);
    Task<Result<IEnumerable<NoteResponseDto>>> FilterNotesByIsDoneAsync(bool isDone, CancellationToken cancellationToken);
}