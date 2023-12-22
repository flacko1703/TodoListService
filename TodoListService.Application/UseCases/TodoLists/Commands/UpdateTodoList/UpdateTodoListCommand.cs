using FluentResults;
using MediatR;
using TodoListService.Application.DTOs.Request.TodoList;
using TodoListService.Application.DTOs.Response;

namespace TodoListService.Application.UseCases.TodoLists.Commands.UpdateTodoList;

public record UpdateTodoListCommand(UpdateTodoListRequestDto UpdateTodoListRequestDto) 
    : IRequest<Result<TodoListResponseDto?>>;