using TodoListService.Domain.Aggregates.TodoListAggregate.Entities;
using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;
using TodoListService.Domain.DomainEvents;
using TodoListService.Domain.DomainEvents.TagDomainEvents;
using TodoListService.Domain.DomainEvents.TaskEntryDomainEvents;
using TodoListService.Domain.DomainEvents.TodoListDomainEvents;
using TodoListService.Domain.Exceptions.TaskEntry;
using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.Aggregates.TodoListAggregate;

public sealed record TodoList : AggregateRoot
{
    private TodoListTitle _title;
    private List<TaskEntry>? _taskEntries = new();
    
    private TodoList(TodoListId id, TodoListTitle title, List<TaskEntry> taskEntries)
        : this(id, title)
    {
        Id = id;
        _title = title;
        _taskEntries = taskEntries;
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

    public IReadOnlyList<TaskEntry>? TaskEntries => _taskEntries?.AsReadOnly();
    
    public static TodoList Create(TodoListTitle title, List<TaskEntry> taskEntries)
    {
        var todoList = new TodoList(Guid.NewGuid(), title, taskEntries);
        todoList.RaiseDomainEvent(new TodoListCreatedDomainEvent(todoList.Id));
        return todoList; 
    }
    
    public static TodoList CreateDefault(TodoListTitle title)
    {
        var todoList = new TodoList(Guid.NewGuid(), title);
        todoList.RaiseDomainEvent(new TodoListCreatedDomainEvent(todoList.Id));
        return todoList;
    }
    
    public void UpdateTodoList(TodoListTitle title)
    {
        _title = title;
        RaiseDomainEvent(new TodoListUpdatedDomainEvent(Id));
    }
    
    public void UpdateTodoList(TodoListTitle title, IEnumerable<TaskEntry>? taskEntries)
    {
        _title = title;
        _taskEntries = taskEntries?.ToList();
        RaiseDomainEvent(new TodoListUpdatedDomainEvent(Id));
    }
    
    public TodoList AddTaskEntry(TaskEntry taskEntry)
    {
        var isExists = _taskEntries.Any(t => t.Id == taskEntry.Id);
        
        if (isExists)
        {
            throw new TaskEntryAlreadyExistsException(taskEntry.Id);
        }
        
        _taskEntries?.Add(taskEntry);
        
        RaiseDomainEvent(new TaskEntryCreatedDomainEvent(taskEntry.Id));
        
        return this;
    }
    
    public void AddTaskEntries(IEnumerable<TaskEntry> taskEntries)
    {
        foreach (var entry in taskEntries)
        {
            AddTaskEntry(entry);
        }
    }
    
    public void AddTagToTaskEntry(TaskEntryId taskEntryId, Tag tag)
    {
        var taskEntry = _taskEntries?.FirstOrDefault(t => t.Id == taskEntryId.Value);
        
        if (taskEntry is null)
        {
            throw new TaskEntryNotFoundException(Id);
        }
        
        taskEntry.AddTag(tag);
        
        RaiseDomainEvent(new TagCreatedDomainEvent(tag.Id));
    }
    
    
    public void RemoveTagFromTaskEntry(TaskEntryId taskEntryId, TagId tagId)
    {
        var taskEntry = _taskEntries?.FirstOrDefault(t => t.Id == taskEntryId.Value);
        
        if (taskEntry is null)
        {
            throw new TaskEntryNotFoundException(Id);
        }
        
        taskEntry.RemoveTag(tagId);
        
        RaiseDomainEvent(new TagRemovedDomainEvent(tagId));
    }
    
    public void RemoveTaskEntry(TaskEntryId taskEntryId)
    {
        var taskEntry = _taskEntries?.FirstOrDefault(t => t.Id == taskEntryId.Value);
        
        if (taskEntry is null)
        {
            throw new TaskEntryNotFoundException(Id);
        }
        
        _taskEntries?.Remove(taskEntry);
        
        RaiseDomainEvent(new TaskEntryRemovedDomainEvent(taskEntryId));
    }
    
    public void UpdateTaskEntry(TaskEntry taskEntry)
    {
        var entry = _taskEntries?.FirstOrDefault(t => t.Id == taskEntry.Id);
        
        if (entry is null)
        {
            throw new TaskEntryNotFoundException(entry?.Id);
        }
        
        entry.Update(taskEntry);
        
        RaiseDomainEvent(new TaskEntryUpdatedDomainEvent(taskEntry.Id));
    }
    
    public void RemoveAllTaskEntries(IEnumerable<TaskEntryId> taskEntryIds)
    {
        foreach (var taskEntryId in taskEntryIds)
        {
            RemoveTaskEntry(taskEntryId);
        }
    }
}