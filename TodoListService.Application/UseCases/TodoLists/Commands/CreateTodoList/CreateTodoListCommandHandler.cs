using FluentResults;
using MediatR;
using TodoListProj.Domain.Aggregates.TodoListAggregate;
using TodoListProj.Domain.Aggregates.TodoListAggregate.ValueObjects;
using TodoListProj.Domain.Repositories;
using TodoListService.Application.Abstractions;

namespace TodoListService.Application.UseCases.TodoLists.Commands.CreateTodoList;

public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, Result<TodoListId>>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTodoListCommandHandler(ITodoListRepository todoListRepository, IUnitOfWork unitOfWork)
    {
        _todoListRepository = todoListRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TodoListId>> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        var todoList = TodoList.CreateDefault(request.Title);
        
        await _todoListRepository.AddAsync(todoList, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return todoList.Id;
    }
}