using FluentResults;
using Mapster;
using MediatR;
using TodoListService.Domain.Repositories;
using TodoListService.Application.DTOs.Response;
using TodoListService.Shared.Abstractions;
using TodoListService.Shared.Messaging.Contracts;

namespace TodoListService.Application.UseCases.TodoLists.Commands.UpdateTodoList;

public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand, Result<TodoListResponseDto?>>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;

    public UpdateTodoListCommandHandler(ITodoListRepository todoListRepository, 
        IUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _todoListRepository = todoListRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }
    
    public async Task<Result<TodoListResponseDto?>> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
    {
        var todoList = await _todoListRepository
            .GetByIdAsync(request.UpdateTodoListRequestDto.Id, cancellationToken);
        
        if (todoList is null)
        {
            return Result.Fail("Todo list not found");
        }
        
        await _todoListRepository.UpdateAsync(todoList, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        await _eventBus.PublishAsync(new TodoListModified
        {
            Id = todoList.Id,
            Title = todoList.Title.Value
        }, cancellationToken);
        
        return Result.Ok(todoList.Adapt<TodoListResponseDto?>());
    }
}