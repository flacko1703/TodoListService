// using FluentResults;
// using Mapster;
// using MediatR;
// using TodoListService.Domain.Repositories;
// using TodoListService.Application.DTOs.Response;
// using TodoListService.Application.Services;
//
// namespace TodoListService.Application.UseCases.TodoLists.Queries.GetNotesByTagQuery;
//
// public class GetNotesByTagQueryHandler : IRequestHandler<GetNotesByTagQuery, Result<IEnumerable<NoteResponseDto>>>
// {
//     private readonly ITodoListOperationService _todoListOperationService;
//     
//     public GetNotesByTagQueryHandler(ITodoListOperationService todoListOperationService)
//     {
//         _todoListOperationService = todoListOperationService;
//     }
//     
//     public async Task<Result<IEnumerable<NoteResponseDto>>> Handle(GetNotesByTagQuery request, 
//         CancellationToken cancellationToken)
//     {
//         var result = await _todoListOperationService.FilterNotesByTagAsync(request.TagId, cancellationToken);
//
//         return Result.Ok(result.Value.Select(x => x.Adapt<NoteResponseDto>()));
//     }
// }