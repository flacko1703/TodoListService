using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Polly;
using Quartz;
using TodoListService.Infrastructure.EF.Contexts;
using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessagesJob(ApplicationDbContext dbContext, 
        IPublisher publisher)
    {
        _dbContext = dbContext;
        _publisher = publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _dbContext.OutboxMessages
            .Where(x => x.SentAt == null)
            .ToListAsync(context.CancellationToken);
        
        foreach (var message in messages)
        {
            IDomainEvent? domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
            
            if (domainEvent is null)
            {
                //Log error
                continue;
            }
            
            var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(2));
            
            var result = await retryPolicy.ExecuteAndCaptureAsync(() =>
                _publisher.Publish(
                    domainEvent, 
                    context.CancellationToken));
            
            
            message.SentAt = DateTime.UtcNow;

            if (result.Outcome == OutcomeType.Failure)
            {
                message.Error = result.FinalException.ToString();
            }
            
            message.Error = "None";
            
            await _dbContext.SaveChangesAsync();
        }
        
        
    }
}