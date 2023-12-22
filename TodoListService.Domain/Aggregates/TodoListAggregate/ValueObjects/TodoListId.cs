using TodoListService.Domain.Exceptions.TodoList;

namespace TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

public record struct TodoListId
{
    private TodoListId (Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new EmptyTodoListIdException();
        }
            
        Value = value;
    }
    
    public Guid Value { get; init; }
    
    public static implicit operator Guid(TodoListId todoListId) => todoListId.Value;
    
    public static implicit operator TodoListId(Guid value) => new(value);
}