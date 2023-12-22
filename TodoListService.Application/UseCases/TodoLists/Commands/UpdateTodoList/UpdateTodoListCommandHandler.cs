using FluentResults;
using Mapster;
using MediatR;
using TodoListProj.Domain.Aggregates.TodoListAggregate.Entities;
using TodoListProj.Domain.Repositories;
using TodoListService.Application.DTOs.Response;

namespace TodoListService.Application.UseCases.TodoLists.Commands.UpdateTodoList;

public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand, Result<TodoListResponseDto?>>
{
    private readonly ITodoListRepository _todoListRepository;

    public UpdateTodoListCommandHandler(ITodoListRepository todoListRepository)
    {
        _todoListRepository = todoListRepository;
    }
    
    public async Task<Result<TodoListResponseDto?>> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
    {
        var todoList = await _todoListRepository.GetByIdAsync(request.UpdateTodoListRequestDto.Id, cancellationToken);
        
        if (todoList is null)
        {
            return Result.Fail(new Error($"Cannot find todolist with Id {request.UpdateTodoListRequestDto.Id}"));
        }
        
        todoList.UpdateTodoList(request.UpdateTodoListRequestDto.Title,
            request.UpdateTodoListRequestDto.Notes.Adapt<List<Note>>());
        
        await _todoListRepository.UpdateAsync(todoList, cancellationToken);
        
        return Result.Ok(request.UpdateTodoListRequestDto.Adapt<TodoListResponseDto?>());
    }
}