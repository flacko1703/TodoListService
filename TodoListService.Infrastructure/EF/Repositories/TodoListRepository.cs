using FluentResults;
using Mapster;
using Microsoft.EntityFrameworkCore;
using TodoListService.Application.Abstractions;
using TodoListProj.Domain.Aggregates.TodoListAggregate;
using TodoListProj.Domain.Aggregates.TodoListAggregate.Entities;
using TodoListProj.Domain.Aggregates.TodoListAggregate.ValueObjects;
using TodoListProj.Domain.Repositories;
using TodoListService.Infrastructure.EF.CompiledQueries;
using TodoListService.Infrastructure.EF.Contexts;

namespace TodoListService.Infrastructure.EF.Repositories;

public class TodoListRepository : ITodoListRepository
{
    private readonly TodoListDbContext _todoListDbContext;

    public TodoListRepository(TodoListDbContext todoListDbContext)
    {
        _todoListDbContext = todoListDbContext;
    }

    public async Task AddAsync(TodoList todoList, CancellationToken cancellationToken = default)
    {
        await _todoListDbContext.TodoLists.AddAsync(todoList, cancellationToken);
    }

    public async Task<IEnumerable<TodoList>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _todoListDbContext.TodoLists
            .Include(x => x.Notes!)
            .ThenInclude(x => x.Tags)
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);
        
        return result;
    }

    public async Task<TodoList?> GetByIdAsync(TodoListId id, CancellationToken cancellationToken = default)
    {
        var todoList = await _todoListDbContext.TodoLists
            .Include(x => x.Notes!)
            .ThenInclude(x => x.Tags)
            .Where(x => x.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return todoList ?? (todoList.ToResult().IsFailed ? null : todoList);
    }

    public async Task UpdateAsync(TodoList todoList, CancellationToken cancellationToken = default)
    {
        var todoListToUpdate = await _todoListDbContext.TodoLists
            .FirstOrDefaultAsync(x => x.Id == todoList.Id, cancellationToken);
        
        if (todoListToUpdate is null)
        {
            return;
        }

        todoListToUpdate.UpdateTodoList(todoList.Title, todoList.Notes?.ToList().Adapt<List<Note>>());
        
        _todoListDbContext.TodoLists.Update(todoListToUpdate);
    }

    public async Task DeleteAsync(TodoListId id, CancellationToken cancellationToken = default)
    {
        var todoList = await _todoListDbContext.TodoLists
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        if (todoList is null)
        {
            return;
        }
        
        _todoListDbContext.TodoLists.Remove(todoList);
    }

    public Task AddNoteAsync(TodoListId todoListId, Note note, CancellationToken cancellationToken = default)
    {
        var todoList = _todoListDbContext.TodoLists
            .Include(x => x.Notes)
            .FirstOrDefault(x => x.Id == todoListId);

        if (todoList is null)
        {
            return Task.CompletedTask;
        }
        
        todoList.AddNote(note);
        
        _todoListDbContext.TodoLists.Update(todoList);
        
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Tag>> GetNoteTagsAsync(NoteId noteId, CancellationToken cancellationToken = default)
    {
        var note = await _todoListDbContext.TodoLists
            .SelectMany(x => x.Notes!)
            .Where(x => x.Id == noteId)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
        
        return note?.Tags ?? Enumerable.Empty<Tag>();
    }

    public async Task<IEnumerable<Note>> GetNotesAsync(TodoListId id, CancellationToken cancellationToken = default)
    {
        var todoList = await _todoListDbContext.TodoLists
            .Include(x => x.Notes!)
            .ThenInclude(x => x.Tags)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        
        return todoList?.Notes ?? Enumerable.Empty<Note>();
    }

    public async Task AddTagAsync(NoteId noteId, Tag tag, CancellationToken cancellationToken = default)
    {
        var todoList = await _todoListDbContext.TodoLists
            .Include(x => x.Notes!)
            .ThenInclude(x => x.Tags)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Notes!.Any(n => n.Id == noteId), cancellationToken: cancellationToken);
            
        if (todoList is null)
        {
            return;
        }

        var note = todoList.Notes!
            .FirstOrDefault(x => x.Id == noteId);
        
        note?.AddTag(tag);
        
        _todoListDbContext.TodoLists.Update(todoList);
    }

    public Task RemoveTagFromNoteAsync(NoteId noteId, TagId tagId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RemoveNoteAsync(TodoListId id, NoteId noteId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    
}