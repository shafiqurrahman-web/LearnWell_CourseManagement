using LearnWell.CourseManagement.Domain.Entities.Classes;
using LearnWell.CourseManagement.Domain.Entities.Users;
using LearnWell.CourseManagement.Domain.Entities.Courses;

namespace LearnWell.CourseManagement.Domain.Entities.CourseClasses
{
    public class CourseClass
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Guid ClassId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        

        public Course Course { get; set; } = default!;
        public Class Class { get; set; } = default!;
        public User? Creator { get; set; }
    }
}
