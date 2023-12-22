using MediatR;
using TodoListService.Application.DTOs.Response;
using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

namespace TodoListService.Application.UseCases.TodoLists.Queries.FilterTaskEntriesByTag;

public record FilterTaskEntriesByTagCommand(TagId Id) : IRequest<IEnumerable<TaskEntryResponseDto>>;