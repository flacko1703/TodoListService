using FluentResults;
using MediatR;
using TodoListService.Application.DTOs.Response;
using TodoListService.Domain.Repositories;
using TodoListService.Shared.Abstractions;
using TodoListService.Shared.Messaging.Contracts;

namespace TodoListService.Application.UseCases.TodoLists.Commands.DeleteTodoListById;

public class DeleteTodoListByIdCommandHandler : IRequestHandler<DeleteTodoListByIdCommand, Result<TodoListResponseDto>>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;
    
    public DeleteTodoListByIdCommandHandler(ITodoListRepository todoListRepository, IUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _todoListRepository = todoListRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task<Result<TodoListResponseDto>> Handle(DeleteTodoListByIdCommand request, CancellationToken cancellationToken)
    {
        await _todoListRepository.DeleteAsync(request.Id, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        await _eventBus.PublishAsync(new TodoListDeleted
        {
            Id = request.Id
        }, cancellationToken);

        return Result.Ok();
    }
}