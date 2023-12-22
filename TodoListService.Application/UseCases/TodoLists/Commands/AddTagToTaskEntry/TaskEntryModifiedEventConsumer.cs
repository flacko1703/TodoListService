using MassTransit;
using Microsoft.Extensions.Logging;
using TodoListService.Shared.Messaging.Contracts;

namespace TodoListService.Application.UseCases.TodoLists.Commands.AddTagToTaskEntry;

public class TaskEntryModifiedEventConsumer : IConsumer<TaskEntryModified>
{
    private readonly ILogger<TaskEntryModifiedEventConsumer> _logger;
    
    public TaskEntryModifiedEventConsumer(ILogger<TaskEntryModifiedEventConsumer> logger)
    {
        _logger = logger;
    }
    
    public Task Consume(ConsumeContext<TaskEntryModified> context)
    {
        _logger.LogInformation("TaskEntry with id: {EventId} was modified", context.Message.Id);
        
        return Task.CompletedTask;
    }
}