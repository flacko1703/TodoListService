using FluentResults;
using MediatR;
using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

namespace TodoListService.Application.UseCases.TodoLists.Commands.DeleteNoteByIdCommand;

public record DeleteNoteByIdCommand(TodoListId TodoListId, TaskEntryId TaskEntryId) : IRequest<Result>;