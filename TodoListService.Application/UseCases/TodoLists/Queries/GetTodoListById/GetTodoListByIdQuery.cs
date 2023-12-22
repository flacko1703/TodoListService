using FluentResults;
using MediatR;
using TodoListService.Application.DTOs.Response;

namespace TodoListService.Application.UseCases.TodoLists.Queries.GetTodoListById;

public record GetTodoListByIdQuery(Guid Id) : IRequest<Result<TodoListResponseDto>>;