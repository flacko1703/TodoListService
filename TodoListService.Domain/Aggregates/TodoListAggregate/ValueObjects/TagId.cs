using TodoListService.Domain.Exceptions;
using TodoListService.Domain.Exceptions.Tag;

namespace TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

public record struct TagId
{
    private TagId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new EmptyTagIdException();
        }
            
        Value = value;
    }
    
    public Guid Value { get; init; }
    
    public static implicit operator Guid(TagId tagId) => tagId.Value;
    
    public static implicit operator TagId(Guid value) => new(value);
    
}