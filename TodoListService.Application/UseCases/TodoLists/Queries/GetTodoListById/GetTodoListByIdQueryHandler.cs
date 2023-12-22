using FluentResults;
using Mapster;
using MediatR;
using TodoListService.Domain.Repositories;
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
        
        if (todoList is null)
        {
            return Result.Fail<TodoListResponseDto>("TodoList not found");
        }
        
        var result = new TodoListResponseDto(todoList.Id, 
            todoList.Title, 
            todoList.TaskEntries?.Select(x => x.Adapt<TaskEntryResponseDto>()).ToList());
        
        return result;
    }
}