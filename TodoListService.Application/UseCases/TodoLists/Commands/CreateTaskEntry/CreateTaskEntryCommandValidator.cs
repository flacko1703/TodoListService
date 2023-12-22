using FluentValidation;

namespace TodoListService.Application.UseCases.TodoLists.Commands.CreateTaskEntry;

public class CreateTaskEntryCommandValidator : AbstractValidator<CreateTaskEntryCommand>
{
    public CreateTaskEntryCommandValidator()
    {
        RuleFor(x => x.TodoListId)
            .NotEmpty()
            .WithMessage("TodoListId is required");

        RuleFor(x => x.TaskEntryRequestDto)
            .NotNull()
            .WithMessage("TaskEntryRequestDto is required");

        RuleFor(x => x.TaskEntryRequestDto.Title)
            .NotEmpty()
            .WithMessage("Title is required");
    }
    
}