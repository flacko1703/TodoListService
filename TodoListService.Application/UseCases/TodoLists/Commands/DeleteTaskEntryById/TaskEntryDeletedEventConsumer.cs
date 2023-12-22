using MassTransit;
using Microsoft.Extensions.Logging;
using TodoListService.Shared.Messaging.Contracts;

namespace TodoListService.Application.UseCases.TodoLists.Commands.DeleteTaskEntryById;

public class TaskEntryDeletedEventConsumer : IConsumer<TaskEntryDeleted>
{
    private readonly ILogger<TaskEntryDeletedEventConsumer> _logger;
    
    public TaskEntryDeletedEventConsumer(ILogger<TaskEntryDeletedEventConsumer> logger)
    {
        _logger = logger;
    }
    
    
    public Task Consume(ConsumeContext<TaskEntryDeleted> context)
    {
        _logger.LogInformation("TaskEntry with id: {EventId} was deleted", context.Message.Id);
        
        return Task.CompletedTask;
    }
}