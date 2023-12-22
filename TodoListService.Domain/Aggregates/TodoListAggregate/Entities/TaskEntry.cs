using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;
using TodoListService.Domain.Enum;
using TodoListService.Domain.Exceptions.Tag;
using TodoListService.Domain.SeedWork;

namespace TodoListService.Domain.Aggregates.TodoListAggregate.Entities;

public record Note : Entity
{
    private NoteTitle _title;
    private NoteText? _text;
    private IList<Tag>? _tags = new List<Tag>();
    private Status _status;
    
    private Note(NoteId id, 
        NoteTitle title, 
        NoteText? text, 
        List<Tag>? tags,
        Status status = Status.Created) : this(id, title, text)
    {
        Id = id;
        _title = title;
        _text = text;
        _tags = tags;
        _status = status;
    }
    
    private Note(NoteId id, 
        NoteTitle title, 
        NoteText? text) : base(id)
    {
        Id = id;
        _title = title;
        _text = text;
    }
    
    private Note()
    {
        //For Entity Framework
    }
    
    public NoteTitle Title => _title;
    
    public NoteText? Text => _text;

    public IReadOnlyList<Tag>? Tags => _tags?.AsReadOnly();
    
    public Status Status => _status;
    
    public static Note Create(NoteTitle noteTitle, 
        NoteText? noteText, IEnumerable<Tag>? tags = default!)
    {
        if (tags is null)
        {
            CreateDefault(noteTitle, noteText);
        }
        
        var task = new Note(Guid.NewGuid(), 
            noteTitle, 
            noteText,
            tags?.ToList());
        return task;
    }
    
    public static Note CreateDefault(NoteTitle noteTitle, 
        NoteText? noteText)
    {
        var task = new Note(Guid.NewGuid(), 
            noteTitle, 
            noteText);
        return task;
    }
    public void UpdateNote(Note note)
    {
        _title = note.Title;
        _text = note.Text;
        _tags = note.Tags?.ToList();
        _status = note.Status;
    }
    
    public Status GetStatus()
    {
        return _status;
    }
    
    public void UpdateStatus(Status status)
    {
        _status = status;
    }
    
    
    public IEnumerable<Tag>? GetTags()
    {
        return _tags;
    }
    
    public NoteTitle GetTitle()
    {
        return _title;
    }
    
    public NoteText? GetText()
    {
        return _text;
    }
    
    public void UpdateTitle(NoteTitle title)
    {
        _title = title;
    }
    
    public void UpdateText(NoteText text)
    {
        _text = text;
    }
    
    public void AddTag(Tag tag)
    {
        _tags?.Add(tag);
    }
    
    public void RemoveTag(TagId tagId)
    {
        var tagToDelete = _tags?.FirstOrDefault(x => x.Id.Equals(tagId));
        
        if (tagToDelete is null)
        {
            throw new TagNotFoundException(tagId);
        }
        
        _tags?.Remove(tagToDelete);
    }
    
}