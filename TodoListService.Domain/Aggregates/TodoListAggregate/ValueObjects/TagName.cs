using TodoListService.Domain.Exceptions;
using TodoListService.Domain.Exceptions.Tag;

namespace TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

public record TagName
{
    private TagName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyTagException();
        }

        Value = value;
    }
    
    public string Value { get; init; }
    
    public static implicit operator string(TagName tagName) => tagName.Value;
    
    public static implicit operator TagName(string tag) => new(tag);
}