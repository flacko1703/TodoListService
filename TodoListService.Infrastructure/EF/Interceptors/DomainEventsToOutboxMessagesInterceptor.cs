using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using TodoListService.Infrastructure.Outbox;
using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Infrastructure.EF.Interceptors;

public class DomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        
        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var messages = dbContext.ChangeTracker.Entries<AggregateRoot>()
            .Select(x => x.Entity)
            .SelectMany(aggregateRoot =>
            {
                var domainEvents = aggregateRoot.DomainEvents;
                aggregateRoot.ClearEvents();
                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage
            {
                Id = Guid.NewGuid(),
                Type = domainEvent.GetType().Name,
                Content = JsonConvert.SerializeObject(domainEvent,
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }),
                CreatedAt = DateTime.UtcNow,
                SentAt = null,
                Error = string.Empty
            })
            .ToList();
        
        dbContext.Set<OutboxMessage>().AddRange(messages);
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}