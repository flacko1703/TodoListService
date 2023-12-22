using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using TodoListProj.Application.DTOs.Response;
using TodoListProj.Application.UseCases.TodoLists.Commands.CreateTodoList;
using TodoListProj.Application.UseCases.TodoLists.Commands.DeleteTodoListById;
using TodoListProj.Application.UseCases.TodoLists.Commands.UpdateTodoList;
using TodoListProj.Application.UseCases.TodoLists.Queries.GetAllTodoLists;
using TodoListProj.Application.UseCases.TodoLists.Queries.GetNotesByTodoListId;
using TodoListProj.Application.UseCases.TodoLists.Queries.GetTodoListById;

namespace TodoListService.WebApi.Controllers;

public class TodoListController : BaseController
{
    private readonly ISender _sender;
    
    public TodoListController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpGet("/TodoList/GetById/{id:guid}")]
    [EnableQuery]
    public async Task<ActionResult<TodoListResponseDto?>> GetTodoListById(Guid id)
    {
        var result = await _sender.Send(new GetTodoListByIdQuery(id));
        
        return result.IsSuccess 
            ? OkOrNotFound(result.Value) 
            : NotFound();
    }
    
    [HttpGet("/TodoList/Get")]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<TodoListResponseDto>>> GetAllTodoLists()
    {
        var result = await _sender.Send(new GetAllTodoListsQuery());
        
        return result.IsSuccess 
            ? Ok(result.Value) 
            : NotFound();
    }
    
    [HttpPost("/TodoList/Add")]
    public async Task<IActionResult?> AddTodoList(CreateTodoListCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result.Value);
    }
    
    [HttpPost("/TodoList/Update")]
    public async Task<IActionResult> UpdateTodoList(UpdateTodoListCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result.Value);
    }
    
    [HttpPost("/TodoList/Delete/{id:guid}")]
    public async Task<IActionResult> DeleteTodoList(Guid id)
    {
        var result = await _sender.Send(new DeleteTodoListByIdCommand(id));
        return Ok(result.Value);
    }
    
    
    [HttpGet("/TodoList/{id:guid}/GetAllNotes/")]
    public async Task<IActionResult> GetAllNotes(Guid id)
    {
        var result = await _sender.Send(new GetNotesByTodoListIdQuery(id));
        return Ok(result.Value);
    }
    
    
    
    
}