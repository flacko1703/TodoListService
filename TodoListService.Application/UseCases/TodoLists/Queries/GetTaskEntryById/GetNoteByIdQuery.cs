using FluentResults;
using MediatR;
using TodoListService.Application.DTOs.Response;

namespace TodoListService.Application.UseCases.TodoLists.Queries.GetTaskEntryById;

public record GetNoteByIdQuery(Guid TodoListId, Guid NoteId) : IRequest<Result<TaskEntryResponseDto>>;