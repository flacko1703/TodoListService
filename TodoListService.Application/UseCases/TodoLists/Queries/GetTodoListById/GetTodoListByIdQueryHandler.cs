using FluentResults;
using Mapster;
using MediatR;
using TodoListProj.Domain.Repositories;
using TodoListService.Application.DTOs.Response;

namespace TodoListService.Application.UseCases.TodoLists.Queries.GetTodoListById;

public class GetTodoListByIdQueryHandler : IRequestHandler<GetTodoListByIdQuery, Result<TodoListResponseDto>?>
{
    private readonly ITodoListRepository _todoListRepository;


    public GetTodoListByIdQueryHandler(ITodoListRepository todoListRepository)
    {
        _todoListRepository = todoListRepository;
    }

    public async Task<Result<TodoListResponseDto>?> Handle(GetTodoListByIdQuery request, CancellationToken cancellationToken)
    {
        var todoList = await _todoListRepository.GetByIdAsync(request.Id, cancellationToken);

        var result = todoList is null
            ? Result.Fail<TodoListResponseDto>("TodoList not found")
            : Result.Ok(todoList.Adapt<TodoListResponseDto>());
        
        return result;
    }
}