using FluentResults;
using MediatR;
using TodoListService.Application.Abstractions;
using TodoListService.Domain.Repositories;

namespace TodoListService.Application.UseCases.TodoLists.Commands.DeleteNoteByIdCommand;

public class DeleteNoteByIdCommandHandler : IRequestHandler<DeleteTaskEntryByIdCommand, Result>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IUnitOfWork _unitOfWork;


    public DeleteNoteByIdCommandHandler(ITodoListRepository todoListRepository, IUnitOfWork unitOfWork)
    {
        _todoListRepository = todoListRepository;
        _unitOfWork = unitOfWork;
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

        return Result.Ok();
    }
}