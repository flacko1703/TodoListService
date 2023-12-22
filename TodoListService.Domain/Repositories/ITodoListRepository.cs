using FluentResults;
using TodoListService.Domain.Aggregates.TodoListAggregate;
using TodoListService.Domain.Aggregates.TodoListAggregate.Entities;
using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

namespace TodoListService.Domain.Repositories;

public interface ITodoListRepository
{
    Task<TodoList?> GetByIdAsync(TodoListId id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TodoList>?> GetAllAsync(CancellationToken cancellationToken = default);
    Task CreateAsync(TodoList todoList, CancellationToken cancellationToken = default);
    Task<TodoList> UpdateAsync(TodoList todoList, CancellationToken cancellationToken = default);
    Task DeleteAsync(TodoListId id, CancellationToken cancellationToken = default);
}