namespace LearnWell.CourseManagement.Application.Courses.GetCourse
{
    public sealed class CourseResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Description { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
