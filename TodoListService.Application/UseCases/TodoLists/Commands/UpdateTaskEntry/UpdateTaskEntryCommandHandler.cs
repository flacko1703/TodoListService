using FluentResults;
using Mapster;
using MediatR;
using TodoListService.Application.DTOs.Response;
using TodoListService.Domain.Aggregates.TodoListAggregate.Entities;
using TodoListService.Domain.Repositories;
using TodoListService.Shared.Abstractions;

namespace TodoListService.Application.UseCases.TodoLists.Commands.UpdateNoteFromList;

public class UpdateTaskEntryCommandHandler : IRequestHandler<UpdateTaskEntryCommand, Result<TaskEntryResponseDto>>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateTaskEntryCommandHandler(ITodoListRepository todoListRepository, IUnitOfWork unitOfWork)
    {
        _todoListRepository = todoListRepository;
        _unitOfWork = unitOfWork;
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
        
        return Result.Ok(taskEntry.Adapt<TaskEntryResponseDto>());
    }
}