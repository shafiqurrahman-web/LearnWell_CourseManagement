using LearnWell.CourseManagement.Domain.Entities.Students;

namespace LearnWell.CourseManagement.Domain.Entities.Users;
public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Role { get; set; } = default!; // "Staff" or "Student"
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    
    public Student? StudentProfile { get; set; }
}