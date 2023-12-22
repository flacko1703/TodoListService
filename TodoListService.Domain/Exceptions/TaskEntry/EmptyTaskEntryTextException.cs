using TodoListService.Domain.SeedWork;

namespace TodoListService.Domain.Exceptions.Note;

public class EmptyNoteTextException : DomainException
{
    public EmptyNoteTextException() 
        : base($"Note text is empty.")
    {
        
    }
}