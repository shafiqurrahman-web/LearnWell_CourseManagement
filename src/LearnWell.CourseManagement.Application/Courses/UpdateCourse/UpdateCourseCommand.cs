using LearnWell.CourseManagement.Application.Abstractions.Messaging;

namespace LearnWell.CourseManagement.Application.Courses.UpdateCourse
{
    public record UpdateCourseCommand(
        Guid id,
        string Code,
        string Title,
        string Description,
        Guid UpdatedBy) : ICommand<Guid>;
}
