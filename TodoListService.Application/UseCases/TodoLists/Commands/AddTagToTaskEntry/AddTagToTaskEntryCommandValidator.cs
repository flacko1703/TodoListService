using FluentValidation;

namespace TodoListService.Application.UseCases.TodoLists.Commands.AddTagToNote;

public class AddTagToNoteCommandValidator : AbstractValidator<AddTagToTaskEntryCommand>
{
    public AddTagToNoteCommandValidator()
    {
        RuleFor(x => x.TodoListId)
            .NotEmpty()
            .WithMessage("TodoListId is required");

        RuleFor(x => x.TaskEntryId)
            .NotEmpty()
            .WithMessage("NoteId is required");

        RuleFor(x => x.Tag)
            .NotNull()
            .WithMessage("Tag is required");

        RuleFor(x => x.Tag.Name)
            .NotEmpty()
            .WithMessage("TagName is required");
    }
    
}