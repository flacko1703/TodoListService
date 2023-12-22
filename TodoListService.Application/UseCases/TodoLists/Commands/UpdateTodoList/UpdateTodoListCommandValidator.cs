using FluentValidation;
using FluentValidation.Validators;
using TodoListService.Application.DTOs.Request.TaskEntries;

namespace TodoListService.Application.UseCases.TodoLists.Commands.UpdateTodoList;

public class UpdateTodoListCommandValidator : AbstractValidator<UpdateTodoListCommand>
{
    public UpdateTodoListCommandValidator()
    {
        RuleFor(x => x.UpdateTodoListRequestDto.Id)
            .NotEmpty()
            .WithMessage("Id is required");

        RuleFor(x => x.UpdateTodoListRequestDto.Title)
            .NotEmpty()
            .WithMessage("Title is required");
        
        RuleForEach(x => x.UpdateTodoListRequestDto.Notes)
            .SetValidator(new UpdateNoteRequestDtoValidator());
    }
    
}

public class UpdateNoteRequestDtoValidator : IPropertyValidator<UpdateTodoListCommand, UpdateTaskEntryRequestDto>
{
    public bool IsValid(ValidationContext<UpdateTodoListCommand> context, UpdateTaskEntryRequestDto? value)
    {
        return value is not null;
    }

    public string GetDefaultMessageTemplate(string errorCode)
    {
        return "Note is required";
    }

    public string Name { get; }
}