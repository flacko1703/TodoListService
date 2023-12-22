using TodoListService.Domain.Aggregates.TodoListAggregate;
using TodoListService.Domain.Aggregates.TodoListAggregate.Entities;
using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

namespace TodoListService.Domain.Repositories;

public interface ITodoListRepository
{
    Task<TodoList?> GetByIdAsync(TodoListId id, CancellationToken cancellationToken = default);
    Task AddAsync(TodoList todoList, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<TodoList>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task UpdateAsync(TodoList todoList, CancellationToken cancellationToken = default);
    
    Task DeleteAsync(TodoListId id, CancellationToken cancellationToken = default);
    
    Task AddNoteAsync(TodoListId todoListId, Note note, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Tag>> GetNoteTagsAsync(NoteId noteId, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Note>> GetNotesAsync(TodoListId id, CancellationToken cancellationToken = default);
    
    Task AddTagAsync(NoteId noteId, Tag tag, CancellationToken cancellationToken = default);
    
    Task RemoveTagFromNoteAsync(NoteId noteId, TagId tagId, CancellationToken cancellationToken = default);
    
    Task RemoveNoteAsync(TodoListId id, NoteId noteId, CancellationToken cancellationToken = default);
    
}