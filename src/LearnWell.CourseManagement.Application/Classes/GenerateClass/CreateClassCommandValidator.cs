using FluentValidation;
namespace LearnWell.CourseManagement.Application.Classes.GenerateClass;

internal sealed class CreateClassCommandValidator : AbstractValidator<CreateClassCommand>
{
    public CreateClassCommandValidator()
    {
        RuleFor(c => c.Code).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
    }
}
