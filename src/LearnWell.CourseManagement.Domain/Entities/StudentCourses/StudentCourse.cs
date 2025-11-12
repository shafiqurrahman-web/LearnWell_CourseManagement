using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.Courses;
using LearnWell.CourseManagement.Domain.Entities.Students;
using LearnWell.CourseManagement.Domain.Entities.Users;

namespace LearnWell.CourseManagement.Domain.Entities.StudentCourses
{
    public sealed class StudentCourse : Entity<StudentCourseId>
    {        
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public Guid? EnrolledBy { get; set; }
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Student Student { get; set; } = default!;
        public Course Course { get; set; } = default!;
        public User StaffUser { get; set; }



    }

}
