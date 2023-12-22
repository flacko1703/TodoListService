using FluentResults;
using Mapster;
using MediatR;
using TodoListService.Application.DTOs.Response;
using TodoListService.Domain.Aggregates.TodoListAggregate.Entities;
using TodoListService.Domain.Repositories;
using TodoListService.Shared.Abstractions;
using TodoListService.Shared.Messaging.Contracts;

namespace TodoListService.Application.UseCases.TodoLists.Commands.CreateTaskEntry;

public class CreateTaskEntryCommandHandler : IRequestHandler<CreateTaskEntryCommand, Result<TaskEntryResponseDto>>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;

    public CreateTaskEntryCommandHandler(ITodoListRepository todoListRepository, 
        IUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _todoListRepository = todoListRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }


    public async Task<Result<TaskEntryResponseDto>> Handle(CreateTaskEntryCommand request, CancellationToken cancellationToken)
    {
        var todoList = await _todoListRepository.GetByIdAsync(request.TodoListId, cancellationToken);
        
        if (todoList is null)
        {
            return Result.Fail<TaskEntryResponseDto>("Todolist not found");
        }
        
        var taskEntry = TaskEntry.Create(request.TaskEntryRequestDto.Title, 
            request.TaskEntryRequestDto.Text,
            request.TaskEntryRequestDto.Tags.Select(x => Tag.Create(x)).ToList());
        
        todoList.AddTaskEntry(taskEntry);
        
        await _todoListRepository.UpdateAsync(todoList, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        await _eventBus.PublishAsync(new TaskEntryCreated
        {
            TodoListId = todoList.Id,
            TaskEntryId = taskEntry.Id,
            Title = taskEntry.Title.Value,
            Text = taskEntry.Text.Value,
            Status = taskEntry.Status.ToString(),
        }, cancellationToken);
        
        var result = taskEntry.Adapt<TaskEntryResponseDto>();
        
        return result;
    }
}