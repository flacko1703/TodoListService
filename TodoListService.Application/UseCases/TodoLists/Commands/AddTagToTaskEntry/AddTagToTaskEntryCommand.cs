using FluentResults;
using MediatR;
using TodoListService.Application.DTOs.Request.Tags;
using TodoListService.Application.DTOs.Response;
using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

namespace TodoListService.Application.UseCases.TodoLists.Commands.AddTagToTaskEntry;

public record AddTagToTaskEntryCommand(TodoListId TodoListId, TaskEntryId TaskEntryId, CreateTagRequestDto Tag) 
    : IRequest<Result<TaskEntryResponseDto>>;