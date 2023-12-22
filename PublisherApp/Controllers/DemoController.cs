using MediatR;
using Microsoft.AspNetCore.Mvc;
using PublisherApp.Command;
using TodoListService.Shared.Messaging.Contracts;

namespace PublisherApp.Controllers;


[ApiController, Route("api/[controller]")]
public class DemoController : ControllerBase
{
    private readonly ISender _sender;

    public DemoController(ISender sender)
    {
        _sender = sender;
    }


    [HttpPost]
    public async Task<IActionResult> Post(TodoListCreated @event)
    {
        await _sender.Send(new PublishEventCommand(@event));

        return Ok();
    }
}