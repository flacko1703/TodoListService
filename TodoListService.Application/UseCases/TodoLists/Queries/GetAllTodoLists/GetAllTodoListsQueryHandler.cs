using FluentResults;
using Mapster;
using MediatR;
using TodoListProj.Domain.Repositories;
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
        var result = await _todoListRepository.GetAllAsync(cancellationToken);
        
        return result.Adapt<List<TodoListResponseDto>>();
    }
}