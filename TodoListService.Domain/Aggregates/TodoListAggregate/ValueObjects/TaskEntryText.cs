using TodoListService.Domain.Exceptions.Note;

namespace TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

public record TaskEntryText
{
    private TaskEntryText(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyNoteTextException();
        }

        Value = value;
    }
    
    public string? Value { get; init; }
    
    public static implicit operator string?(TaskEntryText taskEntryText) => taskEntryText.Value;
    
    public static implicit operator TaskEntryText(string? value) => new(value);
}