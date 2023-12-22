using FluentResults;
using Mapster;
using MediatR;
using TodoListService.Domain.Repositories;
using TodoListService.Application.DTOs.Response;

namespace TodoListService.Application.UseCases.TodoLists.Queries.GetAllTodoLists;

public class GetAllTodoListsQueryHandler : IRequestHandler<GetAllTodoListsQuery, Result<IEnumerable<TodoListResponseDto>>>
{
    private readonly ITodoListRepository _todoListRepository;
    
    public GetAllTodoListsQueryHandler(ITodoListRepository todoListRepository)
    {
        _todoListRepository = todoListRepository;
    }
    
    public async Task<Result<IEnumerable<TodoListResponseDto>>> Handle(GetAllTodoListsQuery request, CancellationToken cancellationToken)
    {
        var todoLists = await _todoListRepository.GetAllAsync(cancellationToken);
        
        if (todoLists is null)
        {
            return Result.Fail<IEnumerable<TodoListResponseDto>>("Cannot find any todolists");
        }
        
        var result = todoLists.Select(x => x.Adapt<TodoListResponseDto>());
        
        return Result.Ok(result);
    }
}