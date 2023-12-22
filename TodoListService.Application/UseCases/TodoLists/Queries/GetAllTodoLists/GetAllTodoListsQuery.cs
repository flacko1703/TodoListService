using FluentResults;
using MediatR;
using TodoListService.Application.DTOs.Response;

namespace TodoListService.Application.UseCases.TodoLists.Queries.GetAllTodoLists;

public record GetAllTodoListsQuery : IRequest<Result<IEnumerable<TodoListResponseDto>>>;