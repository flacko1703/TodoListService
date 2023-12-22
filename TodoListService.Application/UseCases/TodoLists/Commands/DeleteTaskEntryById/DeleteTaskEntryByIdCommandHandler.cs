using FluentResults;
using MediatR;
using TodoListService.Domain.Repositories;
using TodoListService.Shared.Abstractions;
using TodoListService.Shared.Messaging.Contracts;

namespace TodoListService.Application.UseCases.TodoLists.Commands.DeleteTaskEntryById;

public class DeleteTaskEntryByIdCommandHandler : IRequestHandler<DeleteTaskEntryByIdCommand, Result>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;


    public DeleteTaskEntryByIdCommandHandler(ITodoListRepository todoListRepository, IUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _todoListRepository = todoListRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task<Result> Handle(DeleteTaskEntryByIdCommand request, CancellationToken cancellationToken)
    {
        var todoList = await _todoListRepository.GetByIdAsync(request.TodoListId, cancellationToken);
        
        if (todoList is null)
        {
            return Result.Fail("Todolist not found");
        }
        
        todoList.RemoveTaskEntry(request.TaskEntryId);
        
        await _todoListRepository.UpdateAsync(todoList, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        await _eventBus.PublishAsync(new TaskEntryDeleted
        {
            Id = request.TaskEntryId.Value,
            TodoListId = todoList.Id
        }, cancellationToken);

        return Result.Ok();
    }
}