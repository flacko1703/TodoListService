using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;
using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.Exceptions.Tag;

public class TagAlreadyExistsException : DomainException
{
    public TagAlreadyExistsException(TagId tagId, TagName tagName) 
        : base($"Tag with id {tagId} and text {tagName} already exists.")
    {
    }
}