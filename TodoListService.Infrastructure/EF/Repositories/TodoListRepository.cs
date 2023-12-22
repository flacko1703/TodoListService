using Microsoft.EntityFrameworkCore;
using TodoListService.Domain.Aggregates.TodoListAggregate;
using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;
using TodoListService.Domain.Repositories;
using TodoListService.Infrastructure.EF.CompiledQueries;
using TodoListService.Infrastructure.EF.Contexts;

namespace TodoListService.Infrastructure.EF.Repositories;

public class TodoListRepository : ITodoListRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TodoListRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(TodoList todoList, CancellationToken cancellationToken = default)
    {
        await _dbContext.TodoLists.AddAsync(todoList, cancellationToken);
    }

    public async Task<IEnumerable<TodoList>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var todoList = TodoListCompiledQueries.GetAllTodoListsAsync(_dbContext);
        
        List<TodoList> todoLists = new();
        
        await foreach (var todolist in todoList)
        {
            todoLists.Add(todolist);
        }

        return todoLists;
    }

    public async Task<TodoList?> GetByIdAsync(TodoListId id, CancellationToken cancellationToken = default)
    {
        var todoList = await TodoListCompiledQueries.GetByIdAsync(_dbContext, id);
        
        return todoList ?? null;
    }

    public async Task<TodoList> UpdateAsync(TodoList todoList, CancellationToken cancellationToken = default)
    {
        _dbContext.TodoLists.Update(todoList);
        
        return todoList;
    }
    public async Task DeleteAsync(TodoListId id, CancellationToken cancellationToken = default)
    {
        var todoList = await TodoListCompiledQueries.GetByIdAsync(_dbContext, id);
        
        if (todoList is null)
        {
            return;
        }
        
        _dbContext.TodoLists.Remove(todoList);
    }
}