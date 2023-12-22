using TodoListService.Domain.Exceptions.Note;

namespace TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

public record TaskEntryTitle
{
    private TaskEntryTitle(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyNoteTitleTextException();
        }

        Value = value;
    }
    
    public string Value { get; init; }
    
    public static implicit operator string(TaskEntryTitle taskEntryTitle) => taskEntryTitle.Value;
    
    public static implicit operator TaskEntryTitle(string value) => new(value);
    
}

