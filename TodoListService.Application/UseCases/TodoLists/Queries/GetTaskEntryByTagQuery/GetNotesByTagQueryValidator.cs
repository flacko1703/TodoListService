using FluentValidation;

namespace TodoListService.Application.UseCases.TodoLists.Queries.GetNotesByTagQuery;

public class GetNotesByTagQueryValidator : AbstractValidator<GetNotesByTagQuery>
{
    public GetNotesByTagQueryValidator()
    {
        RuleFor(x => x.TagId)
            .NotEmpty();
    }
}