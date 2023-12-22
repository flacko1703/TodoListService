using FluentResults;
using MediatR;
using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;
using TodoListService.Application.DTOs.Response;

namespace TodoListService.Application.UseCases.TodoLists.Queries.GetNotesByTagQuery;

public record GetNotesByTagQuery(TagId TagId) : IRequest<Result<IEnumerable<TaskEntryResponseDto>>>;