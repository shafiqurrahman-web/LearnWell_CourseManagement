namespace LearnWell.CourseManagement.Domain.Entities.Courses;

public record CourseId(Guid Value)
{
    public static CourseId New() => new(Guid.NewGuid());
}
