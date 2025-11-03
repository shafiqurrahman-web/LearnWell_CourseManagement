using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.Students;
using LearnWell.CourseManagement.Domain.Entities.Users.ValueObjects;

namespace LearnWell.CourseManagement.Domain.Entities.Users;
public sealed class User : Entity<UserId>
{

    public string IdentityId { get; private set; } = string.Empty;
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }
    public string Username { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    
    public string Role { get; set; } = default!; // "Staff" or "Student"
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    private readonly List<Role> _roles = new();
    public IReadOnlyCollection<Role> Roles => _roles;

    public Student StudentProfile { get; set; } = new Student();
}