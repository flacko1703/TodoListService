using MassTransit;
using Microsoft.Extensions.Logging;
using TodoListService.Shared.Messaging.Contracts;

namespace TodoListService.Application.UseCases.TodoLists.Commands.DeleteTodoListById;

public class TodoListDeletedEventConsumer : IConsumer<TodoListDeleted>
{
    private readonly ILogger<TodoListDeletedEventConsumer> _logger;
    
    public TodoListDeletedEventConsumer(ILogger<TodoListDeletedEventConsumer> logger)
    {
        _logger = logger;
    }
    
    public Task Consume(ConsumeContext<TodoListDeleted> context)
    {
        _logger.LogInformation("TodoList with id: {EventId} was deleted", context.Message.Id);
        
        return Task.CompletedTask;
    }
}