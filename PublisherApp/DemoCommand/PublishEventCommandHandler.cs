using MediatR;
using TodoListService.Shared.Abstractions;

namespace PublisherApp.Command;

public class PublishEventCommandHandler : IRequestHandler<PublishEventCommand>
{
    private readonly IEventBus _eventBus;

    public PublishEventCommandHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public Task Handle(PublishEventCommand request, CancellationToken cancellationToken)
    {
        return _eventBus.PublishAsync(request.Event, cancellationToken);
    }
}