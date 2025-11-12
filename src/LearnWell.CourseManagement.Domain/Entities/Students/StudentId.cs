namespace LearnWell.CourseManagement.Domain.Entities.Students
{
    public record StudentId(Guid Value)
    {
        public static StudentId New() => new(Guid.NewGuid());
    }

}
