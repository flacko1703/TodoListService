using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using TodoListService.Application.DTOs.Request.Tags;
using TodoListService.Application.DTOs.Request.TaskEntries;
using TodoListService.Application.DTOs.Response;
using TodoListService.Application.UseCases.TodoLists.Commands.AddTagToTaskEntry;
using TodoListService.Application.UseCases.TodoLists.Commands.CreateTaskEntry;
using TodoListService.Application.UseCases.TodoLists.Commands.CreateTodoList;
using TodoListService.Application.UseCases.TodoLists.Commands.DeleteTodoListById;
using TodoListService.Application.UseCases.TodoLists.Commands.UpdateTodoList;
using TodoListService.Application.UseCases.TodoLists.Queries.GetAllTodoLists;
using TodoListService.Application.UseCases.TodoLists.Queries.GetTaskEntryById;
using TodoListService.Application.UseCases.TodoLists.Queries.GetTaskEntryByTodoListId;
using TodoListService.Application.UseCases.TodoLists.Queries.GetTodoListById;

namespace TodoListService.WebApi.Controllers;

[Route("api/[controller]")]
public class TodoListController : BaseController
{
    private readonly ISender _sender;
    
    public TodoListController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpGet("/GetById/{id:guid}")]
    [EnableQuery]
    public async Task<ActionResult<TodoListResponseDto?>> GetTodoListById(Guid id)
    {
        var result = await _sender.Send(new GetTodoListByIdQuery(id));
        
        return result.IsSuccess 
            ? OkOrNotFound(result.Value) 
            : NotFound();
    }
    
    [HttpGet("/GetAllTodoLists")]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<TodoListResponseDto>>> GetAllTodoLists()
    {
        var result = await _sender.Send(new GetAllTodoListsQuery());
        
        return result.IsSuccess 
            ? Ok(result.Value) 
            : NotFound();
    }
    
    [HttpPost("CreateTodoList")]
    public async Task<IActionResult?> CreateTodoList(CreateTodoListCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result.Value);
    }
    
    [HttpPost("UpdateTodoList")]
    public async Task<IActionResult> UpdateTodoList(UpdateTodoListCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result.Value);
    }
    
    [HttpPost("DeleteTodoList/{id:guid}")]
    public async Task<IActionResult> DeleteTodoList(Guid id)
    {
        var result = await _sender.Send(new DeleteTodoListByIdCommand(id));
        return Ok(result.Value);
    }
    
    
    [HttpGet("/{id:guid}/GetNotesFromTodoList/")]
    public async Task<IActionResult> GetNotesFromTodoList(Guid id)
    {
        var result = await _sender.Send(new GetNotesByTodoListIdQuery(id));
        return Ok(result.Value);
    }
    
    [HttpGet("/{id:guid}/GetNoteById/{noteId:guid}")]
    public async Task<IActionResult> GetNoteById(Guid id, Guid noteId)
    {
        var result = await _sender.Send(new GetNoteByIdQuery(id, noteId));
        return Ok(result.Value);
    }
    
    [HttpPost("/{id:guid}/AddNoteToTodoList")]
    public async Task<IActionResult> AddNoteToTodoList(Guid id, CreateTaskEntryRequestDto taskEntry)
    {
        var result = await _sender.Send(new CreateTaskEntryCommand(id, taskEntry));
        return Ok(result.Value);
    }
    
    
    [HttpPost("/{id:guid}/AddTagToNote/{noteId:guid}")]
    public async Task<IActionResult> AddTagToNote(Guid id, Guid noteId, CreateTagRequestDto tag)
    {
        var result = await _sender.Send(new AddTagToTaskEntryCommand(id, noteId, tag));
        return Ok(result.Value);
    }
    
    
    
    
    
}