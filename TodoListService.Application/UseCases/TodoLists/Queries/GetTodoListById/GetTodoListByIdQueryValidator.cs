using FluentValidation;

namespace TodoListService.Application.UseCases.TodoLists.Queries.GetTodoListById;

public class GetTodoListByIdQueryValidator : AbstractValidator<GetTodoListByIdQuery>
{
    public GetTodoListByIdQueryValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Id is required.");
    }   
}