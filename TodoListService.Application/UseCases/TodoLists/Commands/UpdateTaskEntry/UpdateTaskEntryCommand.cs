using FluentResults;
using MediatR;
using TodoListService.Application.DTOs.Request.TaskEntries;
using TodoListService.Application.DTOs.Response;

namespace TodoListService.Application.UseCases.TodoLists.Commands.UpdateTaskEntry;

public record UpdateTaskEntryCommand(UpdateTaskEntryRequestDto UpdateTaskEntryRequestDto) 
    : IRequest<Result<TaskEntryResponseDto>>;