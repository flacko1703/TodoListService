using FluentResults;
using MediatR;
using TodoListService.Application.DTOs.Request.TaskEntries;
using TodoListService.Application.DTOs.Response;
using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

namespace TodoListService.Application.UseCases.TodoLists.Commands.CreateTaskEntry;

public record CreateNoteCommand(TodoListId TodoListId, CreateTaskEntryRequestDto TaskEntryRequestDto) 
    : IRequest<Result<TaskEntryResponseDto>>;