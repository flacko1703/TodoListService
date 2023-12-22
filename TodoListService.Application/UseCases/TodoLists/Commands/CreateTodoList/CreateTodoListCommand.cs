using FluentResults;
using MediatR;
using TodoListProj.Domain.Aggregates.TodoListAggregate.ValueObjects;

namespace TodoListService.Application.UseCases.TodoLists.Commands.CreateTodoList;

public sealed record CreateTodoListCommand(TodoListTitle Title) 
    : IRequest<Result<TodoListId>>;