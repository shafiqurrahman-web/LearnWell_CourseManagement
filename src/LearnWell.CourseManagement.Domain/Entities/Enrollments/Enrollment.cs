using LearnWell.CourseManagement.Domain.Entities.Classes;
using LearnWell.CourseManagement.Domain.Entities.Students;
using LearnWell.CourseManagement.Domain.Entities.Users;

namespace LearnWell.CourseManagement.Domain.Entities.Enrollments
{
    public class Enrollment
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid ClassId { get; set; }
        public Guid? EnrolledBy { get; set; }
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        public bool FromCourseDefault { get; set; }


        public Student Student { get; set; } = default!;
        public Class Class { get; set; } = default!;
        public User? StaffUser { get; set; }
    }
}
