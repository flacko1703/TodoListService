using TodoListService.Domain.SeedWork;

namespace TodoListService.Domain.Exceptions.Note;

public class EmptyNoteTitleTextException : DomainException
{
    public EmptyNoteTitleTextException() 
        : base($"Note Title text cannot be empty.")
    {
        
    }
}