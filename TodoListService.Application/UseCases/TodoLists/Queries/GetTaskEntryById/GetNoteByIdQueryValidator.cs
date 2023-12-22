using FluentValidation;

namespace TodoListService.Application.UseCases.TodoLists.Queries.GetTaskEntryById;

public class GetNoteByIdQueryValidator : AbstractValidator<GetNoteByIdQuery>
{
    public GetNoteByIdQueryValidator()
    {
        RuleFor(x => x.TodoListId)
            .NotEmpty()
            .WithMessage("TodoListId is required");

        RuleFor(x => x.NoteId)
            .NotEmpty()
            .WithMessage("NoteId is required");
    }
    
}