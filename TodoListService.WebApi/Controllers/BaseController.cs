using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace TodoListService.WebApi.Controllers;

[ApiController]
public abstract class BaseController : ODataController
{
    protected ActionResult<TResult?> OkOrNotFound<TResult>(TResult? result)
        => result is null ? NotFound() : Ok(result);
}