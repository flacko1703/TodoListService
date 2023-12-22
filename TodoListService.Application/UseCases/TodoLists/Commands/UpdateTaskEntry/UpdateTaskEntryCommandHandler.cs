using FluentResults;
using Mapster;
using MediatR;
using TodoListService.Application.DTOs.Response;
using TodoListService.Domain.Aggregates.TodoListAggregate.Entities;
using TodoListService.Domain.Repositories;
using TodoListService.Shared.Abstractions;
using TodoListService.Shared.Messaging.Contracts;

namespace TodoListService.Application.UseCases.TodoLists.Commands.UpdateTaskEntry;

public class UpdateTaskEntryCommandHandler : IRequestHandler<UpdateTaskEntryCommand, Result<TaskEntryResponseDto>>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;
    
    public UpdateTaskEntryCommandHandler(ITodoListRepository todoListRepository, IUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _todoListRepository = todoListRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    
    public async Task<Result<TaskEntryResponseDto>> Handle(UpdateTaskEntryCommand request, CancellationToken cancellationToken)
    {
        var todoList = await _todoListRepository
            .GetByIdAsync(request.UpdateTaskEntryRequestDto.TodoListId, cancellationToken);
        
        if (todoList == null)
        {
            return Result.Fail("Todo list not found");
        }
        
        var taskEntry = todoList.TaskEntries.FirstOrDefault(x => x.Id == request.UpdateTaskEntryRequestDto.TaskEntryId);
        
        if (taskEntry == null)
        {
            return Result.Fail("Note not found");
        }
        
        todoList.UpdateTaskEntry(request.UpdateTaskEntryRequestDto.Adapt<TaskEntry>());
        
        await _todoListRepository.UpdateAsync(todoList, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        await _eventBus.PublishAsync(new TaskEntryModified
        {
            Id = taskEntry.Id,
            TodoListId = todoList.Id
        }, cancellationToken);
        
        return Result.Ok(taskEntry.Adapt<TaskEntryResponseDto>());
    }
}