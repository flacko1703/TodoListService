using FluentValidation;

namespace TodoListService.Application.UseCases.TodoLists.Queries.GetTaskEntryByTodoListId;

public class GetNotesByTodoListIdQueryValidator : AbstractValidator<GetNotesByTodoListIdQuery>
{
    public GetNotesByTodoListIdQueryValidator()
    {
        RuleFor(x => x.TodoListId)
            .NotEmpty();
        
    }
}