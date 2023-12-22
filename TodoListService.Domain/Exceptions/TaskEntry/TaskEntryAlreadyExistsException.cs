using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;
using TodoListService.Domain.SeedWork;

namespace TodoListService.Domain.Exceptions.Note;

public class NoteAlreadyExistsException : DomainException
{
    public NoteAlreadyExistsException(TaskEntryId taskEntryId) 
        : base($"Note with id: {taskEntryId} already exists.")
    {
        
    }
}