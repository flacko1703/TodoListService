using FluentResults;
using MediatR;
using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

namespace TodoListService.Application.UseCases.TodoLists.Commands.DeleteTaskEntryById;

public record DeleteTaskEntryByIdCommand(TodoListId TodoListId, TaskEntryId TaskEntryId) : IRequest<Result>;