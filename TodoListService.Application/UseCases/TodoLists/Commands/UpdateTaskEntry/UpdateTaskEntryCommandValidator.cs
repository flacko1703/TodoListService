using FluentValidation;

namespace TodoListService.Application.UseCases.TodoLists.Commands.UpdateNoteFromList;

public class UpdateTaskEntryCommandValidator : AbstractValidator<UpdateTaskEntryCommand>
{
    public UpdateTaskEntryCommandValidator()
    {
        RuleFor(x => x.UpdateTaskEntryRequestDto.TodoListId)
            .NotEmpty()
            .WithMessage("TodoListId is required");

        RuleFor(x => x.UpdateTaskEntryRequestDto.TaskEntryId)
            .NotEmpty()
            .WithMessage("TaskEntryId is required");
        
        RuleFor(x => x.UpdateTaskEntryRequestDto.Title)
            .NotEmpty()
            .WithMessage("Title is required");
    }
    
}