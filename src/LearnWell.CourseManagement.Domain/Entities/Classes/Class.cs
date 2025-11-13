using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.CourseClasses;
using LearnWell.CourseManagement.Domain.Entities.Enrollments;
using LearnWell.CourseManagement.Domain.Entities.Users;

namespace LearnWell.CourseManagement.Domain.Entities.Classes;

public sealed class Class : Entity<ClassId>
{
    public string Code { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; }
    public UserId CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public User Creator { get; set; }
    public ICollection<CourseClass> CourseClasses { get; set; } = new List<CourseClass>();
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
