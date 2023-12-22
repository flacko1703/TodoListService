namespace TodoListService.Domain.SeedWork;

public abstract record Entity
{
    public Guid Id { get; protected init; }

    protected Entity(Guid id)
    {
        Id = id;
    }

    protected Entity()
    {
        
    }
}