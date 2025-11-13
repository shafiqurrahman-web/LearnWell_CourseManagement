using FluentValidation;
namespace LearnWell.CourseManagement.Application.Courses.GenerateCourse
{
    internal sealed class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator()
        {
            RuleFor(c => c.Code).NotEmpty();
            RuleFor(c => c.Title).NotEmpty();
        }
    }

}
