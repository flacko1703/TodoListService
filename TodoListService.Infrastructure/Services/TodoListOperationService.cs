using FluentResults;
using Mapster;
using TodoListService.Application.Abstractions;
using TodoListService.Application.DTOs.Request.Note;
using TodoListService.Application.DTOs.Request.Tag;
using TodoListService.Application.DTOs.Request.TodoList;
using TodoListService.Application.DTOs.Response;
using TodoListService.Application.Services;
using TodoListService.Domain.Aggregates.TodoListAggregate;
using TodoListService.Domain.Aggregates.TodoListAggregate.Entities;
using TodoListService.Domain.Repositories;

namespace TodoListService.Infrastructure.Services;

public class TodoListService : ITodoListService
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly ITodoListService _todoListService;
    private readonly IUnitOfWork _unitOfWork;


    public TodoListService(ITodoListRepository todoListRepository, IUnitOfWork unitOfWork, ITodoListService todoListService)
    {
        _todoListRepository = todoListRepository;
        _unitOfWork = unitOfWork;
        _todoListService = todoListService;
    }


    public async Task<Result<TodoList>> AddNoteAsync(Guid todoListId, CreateNoteRequestDto noteRequestDto, CancellationToken cancellationToken)
    {
        var result = await _todoListService.AddNoteAsync(todoListId, noteRequestDto, cancellationToken);
        
        if (result.IsFailed)
        {
            return Result.Fail<TodoList>(result.Errors.First().Message);
        }
        
        return result;
    }

    public async Task<Result<IEnumerable<NoteResponseDto>>> FilterNotesByTagAsync(Guid tagId, 
        CancellationToken cancellationToken)
    {
        var todoLists = await _todoListRepository.GetAllAsync(cancellationToken);
        
        if (todoLists is null)
        {
            return Result.Fail("Todo lists not found");
        }
        
        var notes = todoLists
            .SelectMany(x => x.Notes)
            .Where(x => x.Tags.Any(x => x.Id == tagId));
        
        return Result.Ok(notes.Select(x => x.Adapt<NoteResponseDto>()));
    }

    public async Task<Result<IEnumerable<NoteResponseDto>>> FilterNotesByIsDoneAsync(bool isDone, 
        CancellationToken cancellationToken)
    {
        var todoLists = await _todoListRepository.GetAllAsync(cancellationToken);
        
        if (todoLists is null)
        {
            return Result.Fail("Todo lists not found");
        }
        
        var notes = todoLists
            .SelectMany(x => x.Notes)
            .Where(x => x.IsDone == isDone);
        
        return Result.Ok(notes.Select(x => x.Adapt<NoteResponseDto>()));
    }
}