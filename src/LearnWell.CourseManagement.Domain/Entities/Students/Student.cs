using LearnWell.CourseManagement.Domain.Entities.Enrollments;
using LearnWell.CourseManagement.Domain.Entities.Users;

namespace LearnWell.CourseManagement.Domain.Entities.Students;

public class Student
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string StudentNumber { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    
    public User User { get; set; } = default!;
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
