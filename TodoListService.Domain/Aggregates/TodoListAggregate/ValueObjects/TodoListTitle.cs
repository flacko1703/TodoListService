using TodoListService.Domain.Exceptions.TodoList;

namespace TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

public record TodoListTitle
{
    public TodoListTitle(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyTodoListTitleException();
        }

        Value = value;
    }
    
    public string Value { get; init; }
    
    public static implicit operator string(TodoListTitle title) => title.Value;
    
    public static implicit operator TodoListTitle(string title) => new(title);
    
}