using TodoListService.Domain.Exceptions;
using TodoListService.Domain.Aggregates.TodoListAggregate.Entities;
using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;
using TodoListService.Domain.DomainEvents;
using TodoListService.Domain.Exceptions.Note;
using TodoListService.Domain.SeedWork;

namespace TodoListService.Domain.Aggregates.TodoListAggregate;

public sealed record TodoList : AggregateRoot<TodoListId>
{
    private TodoListTitle _title;
    private List<Note> _notes = new();
    
    private TodoList(TodoListId id, TodoListTitle title, List<Note> notes)
        : this(id, title)
    {
        Id = id;
        _title = title;
        _notes = notes;
    }
    
    private TodoList(TodoListId id, TodoListTitle title)
    {
        Id = id;
        _title = title;
    }
    
    private TodoList()
    {
        //For Entity Framework
    }
    
    public TodoListTitle Title => _title;

    public IReadOnlyList<Note>? Notes => _notes?.AsReadOnly();
    
    public static TodoList CreateWithNotes(TodoListTitle title, List<Note> notes)
    {
        if (notes.Any())
        {
            CreateDefault(title);
        }
        
        var todoList = new TodoList(Guid.NewGuid(), title, notes);
        todoList.AddDomainEvent(new TodoListCreatedDomainEvent(todoList.Id));
        return todoList; 
    }
    
    public static TodoList CreateDefault(TodoListTitle title)
    {
        var todoList = new TodoList(Guid.NewGuid(), title);
        todoList.AddDomainEvent(new TodoListCreatedDomainEvent(todoList.Id));
        return todoList;
    }
    
    public void UpdateTodoList(TodoListTitle title)
    {
        _title = title;
    }
    
    public void UpdateTodoList(TodoListTitle title, IEnumerable<Note> notes)
    {
        _title = title;
        _notes = notes.ToList();
    }
    
    public void AddNote(Note note)
    {
        var exists = _notes.Any(t => t.Id == note.Id);
        
        if (exists)
        {
            throw new NoteAlreadyExistsException(note.Id);
        }
        
        _notes.Add(note);
        
        AddDomainEvent(new NoteCreatedDomainEvent(Id, note.Id));
    }
    
    public void AddNotes(IEnumerable<Note> notes)
    {
        foreach (var record in notes)
        {
            AddNote(record);
        }
    }
    
    public void RemoveNote(NoteId noteId)
    {
        var note = _notes.FirstOrDefault(t => t.Id == noteId);
        
        if (note is null)
        {
            throw new NoteNotFoundException(Id);
        }
        
        _notes.Remove(note);
        
        AddDomainEvent(new NoteRemovedDomainEvent(Id, noteId));
    }
    
    public void RemoveNotes(IEnumerable<NoteId> todoRecordIds)
    {
        foreach (var todoRecordId in todoRecordIds)
        {
            RemoveNote(todoRecordId);
        }
    }
    
    public void UpdateNote(Note note)
    {
        var recordToUpdate = _notes.FirstOrDefault(t => t.Id == note.Id);
        
        if (recordToUpdate is null)
        {
            throw new NoteNotFoundException(recordToUpdate?.Id);
        }
        
        recordToUpdate.UpdateNote(note);
    }
    
    
    public void RemoveAllRecords()
    {
        _notes.Clear();
    }
    
}