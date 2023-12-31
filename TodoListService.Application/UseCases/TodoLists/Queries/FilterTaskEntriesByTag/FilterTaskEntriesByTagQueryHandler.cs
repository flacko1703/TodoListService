﻿using MediatR;
using TodoListService.Application.DTOs.Response;
using TodoListService.Application.Services;

namespace TodoListService.Application.UseCases.TodoLists.Queries.FilterTaskEntriesByTag;

public class FilterTaskEntriesByTagQueryHandler 
    : IRequestHandler<FilterTaskEntriesByTagQuery, IEnumerable<TaskEntryResponseDto>>
{
    private readonly ITodoListOperationService _todoListOperationService;

    public FilterTaskEntriesByTagQueryHandler(ITodoListOperationService todoListOperationService)
    {
        _todoListOperationService = todoListOperationService;
    }


    public async Task<IEnumerable<TaskEntryResponseDto>> Handle(FilterTaskEntriesByTagQuery request, CancellationToken cancellationToken)
    {
        var result = await _todoListOperationService.FilterTaskEntriesByTagAsync(request.Id, cancellationToken);
        
        return result.Value;
    }
}