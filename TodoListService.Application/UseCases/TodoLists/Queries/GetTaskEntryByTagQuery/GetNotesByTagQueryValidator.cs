using FluentValidation;

namespace TodoListService.Application.UseCases.TodoLists.Queries.GetTaskEntryByTagQuery;

public class GetNotesByTagQueryValidator : AbstractValidator<GetNotesByTagQuery>
{
    public GetNotesByTagQueryValidator()
    {
        RuleFor(x => x.TagId)
            .NotEmpty();
    }
}