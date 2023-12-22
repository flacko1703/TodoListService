using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;
using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.Exceptions.Tag;

public class TagNotFoundException : DomainException
{
    public TagNotFoundException(TagId id) 
        : base($"Tag with id {id} not found.")
    {
    }
}