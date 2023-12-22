using TodoListService.Domain.Exceptions.TaskEntry;

namespace TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

public record TaskEntryTitle
{
    private TaskEntryTitle(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyTaskEntryTitleTextException();
        }

        Value = value;
    }
    
    public string Value { get; init; }
    
    public static implicit operator string(TaskEntryTitle taskEntryTitle) => taskEntryTitle.Value;
    
    public static implicit operator TaskEntryTitle(string value) => new(value);
    
}

