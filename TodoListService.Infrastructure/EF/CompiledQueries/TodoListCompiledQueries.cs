using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TodoListService.Domain.Aggregates.TodoListAggregate;
using TodoListService.Domain.Aggregates.TodoListAggregate.Entities;
using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;
using TodoListService.Infrastructure.EF.Contexts;

namespace TodoListService.Infrastructure.EF.CompiledQueries;

public static class TodoListCompiledQueries
{
    public static readonly Func<ApplicationDbContext, IAsyncEnumerable<TodoList>> GetAllTodoListsAsync =
        Microsoft.EntityFrameworkCore.EF.CompileAsyncQuery((ApplicationDbContext context) =>
            context.TodoLists
                .Include(x => x.TaskEntries)
                .ThenInclude(x => x.Tags)
                .AsNoTracking());


    public static readonly Func<ApplicationDbContext, Guid, Task<TodoList?>> GetByIdAsync =
        Microsoft.EntityFrameworkCore.EF.CompileAsyncQuery((ApplicationDbContext context, Guid id) =>
            context.TodoLists
                .Include(x => x.TaskEntries)
                .ThenInclude(x => x.Tags)
                .AsSplitQuery()
                .FirstOrDefault(x => x.Id == id));
    
    public static readonly Func<ApplicationDbContext, Guid, Task<TodoList?>> GetByIdWithNotesAsync =
        Microsoft.EntityFrameworkCore.EF.CompileAsyncQuery((ApplicationDbContext context, Guid id) =>
            context.TodoLists
                .Include(x => x.TaskEntries)
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefault());
    
    public static readonly Func<ApplicationDbContext, Guid, Task<TodoList?>> GetByIdWithNotesAndTagsAsync =
        Microsoft.EntityFrameworkCore.EF.CompileAsyncQuery((ApplicationDbContext context, Guid id) =>
            context.TodoLists
                .Include(x => x.TaskEntries)!
                .ThenInclude(x => x.Tags)
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefault());
    
    
    public static readonly Func<ApplicationDbContext, Guid, IAsyncEnumerable<TaskEntry>> GetAllNotesAsync =
        Microsoft.EntityFrameworkCore.EF.CompileAsyncQuery((ApplicationDbContext context, Guid id) =>
            context.TodoLists
                .Include(x => x.TaskEntries)!
                .SelectMany(x => x.TaskEntries!)
                .AsNoTracking());
    
    public static readonly Func<ApplicationDbContext, Guid, IAsyncEnumerable<TaskEntry>> GetNotesFromTodoList =
        Microsoft.EntityFrameworkCore.EF.CompileAsyncQuery((ApplicationDbContext context, Guid id) =>
            context.TodoLists
                .Include(x => x.TaskEntries)!
                .ThenInclude(x => x.Tags)
                .Where(x => x.Id == id)
                .SelectMany(x => x.TaskEntries!)
                .AsNoTracking());


    public static readonly Func<ApplicationDbContext, Guid, Guid, Task<TaskEntry?>> GetNoteByIdAsync =
        Microsoft.EntityFrameworkCore.EF.CompileAsyncQuery(
            (ApplicationDbContext context, Guid todoListId, Guid noteId) =>
                context.TodoLists
                    .Include(x => x.TaskEntries)!
                    .Where(x => x.Id == todoListId)
                    .SelectMany(x => x.TaskEntries)
                    .Where(x => x.Id == noteId)
                    .AsNoTracking()
                    .FirstOrDefault());

    public static readonly Func<ApplicationDbContext, IAsyncEnumerable<Tag>> GetAllTagsAsync =
        Microsoft.EntityFrameworkCore.EF.CompileAsyncQuery(
            (ApplicationDbContext context) =>
                context.TodoLists
                    .Include(x => x.TaskEntries)
                    .SelectMany(x => x.TaskEntries)
                    .SelectMany(x => x.Tags ?? Enumerable.Empty<Tag>())
                    .AsNoTracking());

    public static readonly Func<ApplicationDbContext, Guid, Guid, Guid, Task<Tag?>> GetTagByIdAsync =
        Microsoft.EntityFrameworkCore.EF.CompileAsyncQuery(
            (ApplicationDbContext context, Guid todoListId, Guid noteId, Guid tagId) =>
                context.TodoLists
                    .Include(x => x.TaskEntries)!
                    .ThenInclude(x => x.Tags)
                    .SelectMany(x => x.TaskEntries ?? Enumerable.Empty<TaskEntry>())
                    .Where(x => x.Id == noteId)
                    .SelectMany(x => x.Tags ?? Enumerable.Empty<Tag>())
                    .Where(x => x.Id == tagId)
                    .AsNoTracking()
                    .FirstOrDefault());
    

}