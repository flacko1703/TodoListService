using MassTransit;
using Microsoft.Extensions.Logging;
using TodoListService.Shared.Messaging.Contracts;

namespace TodoListService.Application.UseCases.TodoLists.Commands.CreateTaskEntry;

public class CreateTaskEntryEventConsumer : IConsumer<TaskEntryCreated>
{
    private readonly ILogger<CreateTaskEntryEventConsumer> _logger;
    
    public CreateTaskEntryEventConsumer(ILogger<CreateTaskEntryEventConsumer> logger)
    {
        _logger = logger;
    }
    
    
    public Task Consume(ConsumeContext<TaskEntryCreated> context)
    {
        _logger.LogInformation("TaskEntry with id: {EventId} was created", context.Message.TaskEntryId);
        
        return Task.CompletedTask;
    }
}