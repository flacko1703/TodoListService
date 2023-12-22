using TodoListService.Domain.Exceptions.Tag;
using TodoListService.Domain.Exceptions.TaskEntry;

namespace TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

public record struct TaskEntryId
{
    private TaskEntryId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new EmptyTaskEntryIdException();
        }
        
        Value = value;
    }
    
    public Guid Value { get; init; }
    
    public static implicit operator Guid(TaskEntryId taskEntryId) => taskEntryId.Value;
    
    public static implicit operator TaskEntryId(Guid value) => new(value);
    
}