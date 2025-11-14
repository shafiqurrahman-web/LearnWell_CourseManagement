using FluentValidation;
namespace LearnWell.CourseManagement.Application.Classes.UpdateClass
{
    internal sealed class UpdateClassCommandValidator : AbstractValidator<UpdateClassCommand>
    {
        public UpdateClassCommandValidator()
        {
            RuleFor(c => c.Code).NotEmpty();
            RuleFor(c => c.Title).NotEmpty();
        }
    }

}
