using FluentValidation;

namespace TodoListService.Application.UseCases.TodoLists.Queries.GetNotesByTodoListId;

public class GetNotesByTodoListIdQueryValidator : AbstractValidator<GetNotesByTodoListIdQuery>
{
    public GetNotesByTodoListIdQueryValidator()
    {
        RuleFor(x => x.TodoListId)
            .NotEmpty();
        
    }
}