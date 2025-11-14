using LearnWell.CourseManagement.Application.Abstractions.Messaging;

namespace LearnWell.CourseManagement.Application.Courses.DeleteCourse
{
    public record DeleteCourseCommand(
        Guid CourseId) : ICommand<Guid>;


    
}
