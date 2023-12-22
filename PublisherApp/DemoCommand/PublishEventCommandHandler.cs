using MediatR;
using TodoListService.Application.Abstractions;

namespace PublisherApp.Command;

public class PublishEventCommandHandler : IRequestHandler<PublishEventCommand>
{
    private readonly IEventBus _eventBus;
    
    public Task Handle(PublishEventCommand request, CancellationToken cancellationToken)
    {
        return _eventBus.PublishAsync(request.Event, cancellationToken);
    }
}