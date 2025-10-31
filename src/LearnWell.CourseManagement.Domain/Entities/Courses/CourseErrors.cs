
using LearnWell.CourseManagement.Domain.Entities.Abstractions;

namespace LearnWell.CourseManagement.Domain.Entities.Courses;

public static class CourseErrors
{
    public static readonly Error NotFound = new(
        "Course.NotFound",
        "The course with the specified identifier was not found");
    
}
