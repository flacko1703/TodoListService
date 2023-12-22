using TodoListService.Domain.SeedWork;

namespace TodoListService.Domain.Exceptions.TodoList;

public class EmptyTodoListIdException : DomainException
{
    public EmptyTodoListIdException() 
        : base($"Todo list id cannot be empty.")
    {
    }
}