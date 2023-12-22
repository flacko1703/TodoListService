using FluentResults;
using MediatR;
using TodoListService.Application.DTOs.Response;
using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

namespace TodoListService.Application.UseCases.TodoLists.Queries.GetTaskEntryByTagQuery;

public record GetNotesByTagQuery(TagId TagId) : IRequest<Result<IEnumerable<TaskEntryResponseDto>>>;