using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoListService.Infrastructure.EF.Contexts;
using TodoListService.Infrastructure.Outbox;
using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Infrastructure.Idempotency;

public class IdempotentDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    private readonly INotificationHandler<TDomainEvent> _notificationHandler;
    private readonly ApplicationDbContext _dbContext;

    public IdempotentDomainEventHandler(INotificationHandler<TDomainEvent> notificationHandler, 
        ApplicationDbContext dbContext)
    {
        _notificationHandler = notificationHandler;
        _dbContext = dbContext;
    }


    public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
    {
        var consumer = _notificationHandler.GetType().Name;
        
        if (await _dbContext.OutboxMessageConsumers.AnyAsync(x => 
                x.Id == notification.Id && 
                x.Name == consumer, 
                cancellationToken))
        {
            return;
        }
        
        await _notificationHandler.Handle(notification, cancellationToken);
        
        await _dbContext.OutboxMessageConsumers
            .AddAsync(new OutboxMessageConsumer
            {
                Id = notification.Id,
                Name = consumer
            }, cancellationToken);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}