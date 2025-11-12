namespace LearnWell.CourseManagement.Domain.Entities.Classes;

public record ClassId(Guid Value)
{
    public static ClassId New() => new(Guid.NewGuid());
}
