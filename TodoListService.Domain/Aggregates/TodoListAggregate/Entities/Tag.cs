using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;
using TodoListService.Domain.SeedWork;

namespace TodoListService.Domain.Aggregates.TodoListAggregate.Entities;

public sealed record Tag : Entity<TagId>
{
    private TagName? _tagName;
    
    private Tag(TagId id, TagName tagName) : base(id)
    {
        Id = id;
        _tagName = tagName;
    }

    private Tag()
    {
        //For Entity Framework
    }
    
    public TagName TagName => _tagName;
    
    public static Tag Create(TagName tagName) => new(Guid.NewGuid(), tagName);
    
    
    public void DeleteTag()
    {
        _tagName = null;
    }
}
