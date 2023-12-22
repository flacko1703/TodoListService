using FluentValidation;

namespace TodoListService.Application.UseCases.TodoLists.Commands.DeleteNoteByIdCommand;

public class DeleteNoteByIdCommandValidator : AbstractValidator<DeleteTaskEntryByIdCommand>
{
    public DeleteNoteByIdCommandValidator()
    {
        RuleFor(x => x.TodoListId)
            .NotEmpty();
        
        RuleFor(x => x.TaskEntryId)
            .NotEmpty();
    }
}