using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;
using TodoListService.Domain.Enum;
using TodoListService.Domain.Exceptions.Tag;
using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.Aggregates.TodoListAggregate.Entities;

public record TaskEntry : Entity
{
    private TaskEntryTitle _title;
    private TaskEntryText? _text;
    private IList<Tag>? _tags = new List<Tag>();
    private Status _status;
    
    private TaskEntry(TaskEntryId id, 
        TaskEntryTitle title, 
        TaskEntryText? text, 
        IList<Tag>? tags,
        Status status = Status.Created) : this(id, title, text)
    {
        Id = id;
        _title = title;
        _text = text;
        _tags = tags;
        _status = status;
    }
    
    private TaskEntry(TaskEntryId id, 
        TaskEntryTitle title, 
        TaskEntryText? text) : base(id)
    {
        Id = id;
        _title = title;
        _text = text;
    }
    
    private TaskEntry()
    {
        //For Entity Framework
    }
    
    public TaskEntryTitle Title => _title;
    
    public TaskEntryText? Text => _text;

    public IReadOnlyList<Tag>? Tags => _tags?.AsReadOnly();
    
    public Status Status => _status;
    
    public static TaskEntry Create(TaskEntryTitle taskEntryTitle, 
        TaskEntryText? taskEntryText, IEnumerable<Tag>? tags = default!)
    {
        if (tags is null)
        {
            CreateDefault(taskEntryTitle, taskEntryText);
        }
        
        var task = new TaskEntry(Guid.NewGuid(), 
            taskEntryTitle, 
            taskEntryText,
            tags?.ToList());
        return task;
    }
    
    public static TaskEntry CreateDefault(TaskEntryTitle taskEntryTitle, 
        TaskEntryText? taskEntryText)
    {
        var task = new TaskEntry(Guid.NewGuid(), 
            taskEntryTitle, 
            taskEntryText);
        return task;
    }
    public void Update(TaskEntry taskEntry)
    {
        _title = taskEntry.Title;
        _text = taskEntry.Text;
        _tags = taskEntry.Tags?.ToList();
        _status = taskEntry.Status;
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
    
    public TaskEntryTitle GetTitle()
    {
        return _title;
    }
    
    public TaskEntryText? GetText()
    {
        return _text;
    }
    
    public void UpdateTitle(TaskEntryTitle title)
    {
        _title = title;
    }
    
    public void UpdateText(TaskEntryText text)
    {
        _text = text;
    }
    
    public void AddTag(Tag tag)
    {
        _tags ??= new List<Tag>();
        
        var isExist = _tags.Any(x => x.Id.Equals(tag.Id));
        
        if (isExist)
        {
            throw new TagAlreadyExistsException(tag.Id, tag.Name);
        }
        
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