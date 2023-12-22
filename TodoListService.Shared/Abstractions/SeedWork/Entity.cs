namespace TodoListService.Shared.Abstractions.SeedWork;

public abstract record Entity
{
    public Guid Id { get; init; }

    protected Entity(Guid id)
    {
        Id = id;
    }

    protected Entity()
    {
        
    }
}