﻿using FluentValidation;

namespace TodoListService.Application.UseCases.TodoLists.Commands.AddTagToTaskEntry;

public class AddTagToTaskEntryCommandValidator : AbstractValidator<AddTagToTaskEntryCommand>
{
    public AddTagToTaskEntryCommandValidator()
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