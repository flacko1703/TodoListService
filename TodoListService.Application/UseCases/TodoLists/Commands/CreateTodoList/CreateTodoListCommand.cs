using FluentResults;
using MediatR;
using TodoListService.Application.DTOs.Response;
using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

namespace TodoListService.Application.UseCases.TodoLists.Commands.CreateTodoList;

public sealed record CreateTodoListCommand(TodoListTitle Title) 
    : IRequest<Result<TodoListResponseDto>>;