using MediatR;
using TodoListService.Shared.Messaging.Contracts;

namespace PublisherApp.Command;

public record PublishEventCommand(TodoListCreated Event) : IRequest;