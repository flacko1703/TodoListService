using Microsoft.EntityFrameworkCore;
using TodoListService.Domain.Aggregates.TodoListAggregate;
using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;
using TodoListService.Infrastructure.EF.Contexts;

namespace TodoListService.Infrastructure.EF.CompiledQueries;

public static class GetAllTodoListsCompiledQuery
{
    public static readonly Func<ApplicationDbContext, IAsyncEnumerable<TodoList>> GetAllTodoListsAsync =
        Microsoft.EntityFrameworkCore.EF.CompileAsyncQuery((ApplicationDbContext context) =>
            context.Set<TodoList>().AsNoTracking());
    
    
    public static readonly Func<ApplicationDbContext, Guid, Task<TodoList?>> GetByIdAsync =
        Microsoft.EntityFrameworkCore.EF.CompileAsyncQuery((ApplicationDbContext context, Guid id) =>
            context.Set<TodoList>().AsNoTracking().FirstOrDefault(x => x.Id == id));
    
    
}