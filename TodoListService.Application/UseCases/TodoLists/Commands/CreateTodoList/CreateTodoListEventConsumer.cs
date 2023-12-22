using MassTransit;
using Microsoft.Extensions.Logging;
using TodoListService.Shared.Messaging.Contracts;

namespace TodoListService.Application.UseCases.TodoLists.Commands.CreateTodoList;

public class CreateTodoListEventConsumer : IConsumer<TodoListCreated>
{
    private readonly ILogger<CreateTodoListEventConsumer> _logger;

    public CreateTodoListEventConsumer(ILogger<CreateTodoListEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<TodoListCreated> context)
    {
        _logger.LogInformation("TodoList with id: {EventId} was created", context.Message.Id);
        
        return Task.CompletedTask;
    }
}