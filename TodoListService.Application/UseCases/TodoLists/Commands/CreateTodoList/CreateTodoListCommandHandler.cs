using FluentResults;
using Mapster;
using MediatR;
using TodoListService.Application.DTOs.Response;
using TodoListService.Domain.Aggregates.TodoListAggregate;
using TodoListService.Domain.Repositories;
using TodoListService.Shared.Abstractions;
using TodoListService.Shared.Messaging.Contracts;

namespace TodoListService.Application.UseCases.TodoLists.Commands.CreateTodoList;

public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, Result<TodoListResponseDto>>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;


    public CreateTodoListCommandHandler(ITodoListRepository todoListRepository, 
        IUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _todoListRepository = todoListRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task<Result<TodoListResponseDto>> Handle(CreateTodoListCommand request, 
        CancellationToken cancellationToken)
    {
        var todoList = TodoList.CreateDefault(request.Title.Value);
        
        await _todoListRepository.CreateAsync(todoList, cancellationToken);
        
        await _eventBus.PublishAsync(new TodoListCreated
        {
            Id = todoList.Id,
            Title = todoList.Title.Value
        }, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return todoList.Adapt<TodoListResponseDto>();
    }
}