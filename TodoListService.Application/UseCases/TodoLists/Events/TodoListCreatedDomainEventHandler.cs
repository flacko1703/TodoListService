using MediatR;
using Microsoft.Extensions.Logging;
using TodoListService.Domain.DomainEvents.TodoListDomainEvents;
using TodoListService.Domain.Repositories;

namespace TodoListService.Application.UseCases.TodoLists.Events;

public class TodoListCreatedDomainEventHandler : INotificationHandler<TodoListCreatedDomainEvent>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly ILogger<TodoListCreatedDomainEventHandler> _logger;

    public TodoListCreatedDomainEventHandler(ITodoListRepository todoListRepository, 
        ILogger<TodoListCreatedDomainEventHandler> logger)
    {
        _todoListRepository = todoListRepository;
        _logger = logger;
    }

    public async Task Handle(TodoListCreatedDomainEvent @event, CancellationToken cancellationToken = default)
    {
        var todoList = await _todoListRepository.GetByIdAsync(@event.Id, cancellationToken);
        
        if (todoList is null)
        {
            _logger.LogError("TodoList with id: {EventId} was not found", @event.Id);
            return;
        }
        
        _logger.LogInformation("TodoList with id: {EventId} was created", @event.Id);
    }
}