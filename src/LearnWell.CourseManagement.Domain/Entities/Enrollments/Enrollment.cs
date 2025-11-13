using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.Classes;
using LearnWell.CourseManagement.Domain.Entities.Students;
using LearnWell.CourseManagement.Domain.Entities.Users;

namespace LearnWell.CourseManagement.Domain.Entities.Enrollments
{
    public sealed class Enrollment : Entity<EnrollmentId>
    {
        
        public StudentId StudentId { get; set; }
        public ClassId ClassId { get; set; }
        public UserId EnrolledBy { get; set; }
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        public bool FromCourseDefault { get; set; } // true if added via course enrollment

        // Navigation
        public Student Student { get; set; } = default!;
        public Class Class { get; set; } = default!;
        public User StaffUser { get; set; }
    }

}
