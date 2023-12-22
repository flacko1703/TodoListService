using FluentResults;
using MediatR;
using TodoListProj.Domain.Aggregates.TodoListAggregate.ValueObjects;
using TodoListService.Application.DTOs.Response;

namespace TodoListService.Application.UseCases.TodoLists.Commands.DeleteTodoListById;

public record DeleteTodoListByIdCommand(TodoListId Id) : IRequest<Result<TodoListResponseDto>>;