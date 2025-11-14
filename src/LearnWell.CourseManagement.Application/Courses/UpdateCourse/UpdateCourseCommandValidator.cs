using FluentValidation;
namespace LearnWell.CourseManagement.Application.Courses.UpdateCourse
{
    internal sealed class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(c => c.Code).NotEmpty();
            RuleFor(c => c.Title).NotEmpty();
        }
    }

}
