using LearnWell.CourseManagement.Application.Abstractions.Messaging;

namespace LearnWell.CourseManagement.Application.Courses.GenerateCourse
{
    public record CreateCourseCommand(
        
        string Code,
        string Title,
        string Description,
        Guid CreatedBy) : ICommand<Guid>;



}
