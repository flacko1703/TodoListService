using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;
using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.Aggregates.TodoListAggregate.Entities;

public sealed record Tag : Entity
{
    private TagName _name;
    
    private Tag(TagId id, TagName name) : base(id)
    {
        Id = id;
        _name = name;
    }

    private Tag()
    {
        //For Entity Framework
    }
    
    public TagName Name => _name;
    
    public static Tag Create(TagName tagName) => new(Guid.NewGuid(), tagName);
}
