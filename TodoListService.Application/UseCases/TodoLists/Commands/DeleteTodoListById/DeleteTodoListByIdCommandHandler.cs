using FluentResults;
using Mapster;
using MediatR;
using TodoListProj.Domain.Repositories;
using TodoListService.Application.Abstractions;
using TodoListService.Application.DTOs.Response;

namespace TodoListService.Application.UseCases.TodoLists.Commands.DeleteTodoListById;

public class DeleteTodoListByIdCommandHandler : IRequestHandler<DeleteTodoListByIdCommand, Result<TodoListResponseDto>>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTodoListByIdCommandHandler(ITodoListRepository todoListRepository, IUnitOfWork unitOfWork)
    {
        _todoListRepository = todoListRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TodoListResponseDto>> Handle(DeleteTodoListByIdCommand request, CancellationToken cancellationToken)
    {
        var todoList = await _todoListRepository.GetByIdAsync(request.Id, cancellationToken);

        if (todoList is null)
        {
            return Result.Fail(new Error($"Cannot find todolist with Id {request.Id}"));
        }

        await _todoListRepository.DeleteAsync(todoList.Id, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return todoList.Adapt<TodoListResponseDto>();
    }
}