using FluentValidation;

namespace TodoListService.Application.UseCases.TodoLists.Commands.DeleteTaskEntryById;

public class DeleteTaskEntryByIdCommandValidator : AbstractValidator<DeleteTaskEntryByIdCommand>
{
    public DeleteTaskEntryByIdCommandValidator()
    {
        RuleFor(x => x.TodoListId)
            .NotEmpty();
        
        RuleFor(x => x.TaskEntryId)
            .NotEmpty();
    }
}