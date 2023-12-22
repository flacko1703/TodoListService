using Microsoft.EntityFrameworkCore;
using TodoListService.Domain.Aggregates.TodoListAggregate;

namespace TodoListService.Infrastructure.EF.Contexts;

public interface IApplicationDbContext 
{
    DbSet<TodoList> TodoLists { get; set; }
}