namespace LearnWell.CourseManagement.Domain.Entities.StudentCourses;

public record StudentCourseId(Guid Value)
{
    public static StudentCourseId New() => new(Guid.NewGuid());
}
