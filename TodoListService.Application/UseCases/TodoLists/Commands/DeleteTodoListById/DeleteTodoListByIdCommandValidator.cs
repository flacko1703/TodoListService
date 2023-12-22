using FluentValidation;

namespace TodoListService.Application.UseCases.TodoLists.Commands.DeleteTodoListById;

public class DeleteTodoListByIdCommandValidator : AbstractValidator<DeleteTodoListByIdCommand>
{
    public DeleteTodoListByIdCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}