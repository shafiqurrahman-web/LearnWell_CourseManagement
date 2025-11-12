namespace LearnWell.CourseManagement.Domain.Entities.CourseClasses;

public record CourseClassId(Guid Value)
{
    public static CourseClassId New() => new(Guid.NewGuid());
}
