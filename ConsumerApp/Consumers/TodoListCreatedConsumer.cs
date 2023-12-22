using MassTransit;
using TodoListService.Shared.Messaging.Contracts;

namespace ConsumerApp.Consumers;

public class TodoListCreatedConsumer : IConsumer<TodoListCreated>
{
    public Task Consume(ConsumeContext<TodoListCreated> context)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"TodoList with id: {context.Message.Id} was created");
        
        return Task.CompletedTask;
    }
}