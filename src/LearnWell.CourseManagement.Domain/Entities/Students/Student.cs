using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.Enrollments;
using LearnWell.CourseManagement.Domain.Entities.StudentCourses;
using LearnWell.CourseManagement.Domain.Entities.Users;




namespace LearnWell.CourseManagement.Domain.Entities.Students;

public sealed class Student : Entity<StudentId>
{
    public Guid UserId { get; set; }
    public string StudentNumber { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public User User { get; set; } = default!;
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
}
