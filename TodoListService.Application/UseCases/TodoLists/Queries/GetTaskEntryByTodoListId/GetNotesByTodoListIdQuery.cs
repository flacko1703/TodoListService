﻿using FluentResults;
using MediatR;
using TodoListService.Application.DTOs.Response;

namespace TodoListService.Application.UseCases.TodoLists.Queries.GetNotesByTodoListId;

public record GetNotesByTodoListIdQuery(Guid TodoListId) : IRequest<Result<IEnumerable<TaskEntryResponseDto>>>;