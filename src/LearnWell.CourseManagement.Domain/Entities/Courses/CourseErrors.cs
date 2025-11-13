
using LearnWell.CourseManagement.Domain.Entities.Abstractions;

namespace LearnWell.CourseManagement.Domain.Entities.Courses;

public static class CourseErrors
{
    public static readonly Error NotFound = new(
        "Course.NotFound",
        "The course with the specified identifier was not found");


    public static readonly Error NotCreated = new(
        "Course.NotCreated",
        "The course is not created");

}
