namespace LearnWell.CourseManagement.Domain.Entities.Enrollments;

public record EnrollmentId(Guid Value)
{
    public static EnrollmentId New() => new(Guid.NewGuid());
}
