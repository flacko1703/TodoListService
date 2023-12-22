using MassTransit;
using Microsoft.Extensions.Logging;
using TodoListService.Shared.Messaging.Contracts;

namespace TodoListService.Application.UseCases.TodoLists.Commands.UpdateTodoList;

public class TodoListModifiedEventConsumer : IConsumer<TodoListModified>
{
    private readonly ILogger<TodoListModifiedEventConsumer> _logger;
    
    public TodoListModifiedEventConsumer(ILogger<TodoListModifiedEventConsumer> logger)
    {
        _logger = logger;
    }
    
    public Task Consume(ConsumeContext<TodoListModified> context)
    {
        _logger.LogInformation("TodoList with id: {EventId} was modified", context.Message.Id);
        
        return Task.CompletedTask;
    }
}