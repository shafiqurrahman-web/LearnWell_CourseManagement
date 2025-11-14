using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.Classes.Events;
using LearnWell.CourseManagement.Domain.Entities.CourseClasses;
using LearnWell.CourseManagement.Domain.Entities.Courses;
using LearnWell.CourseManagement.Domain.Entities.Courses.Events;
using LearnWell.CourseManagement.Domain.Entities.Enrollments;
using LearnWell.CourseManagement.Domain.Entities.Users;

namespace LearnWell.CourseManagement.Domain.Entities.Classes;

public sealed class Class : Entity<ClassId>
{
    private UserId userId;
    private DateTime utcNow;

    public Class(ClassId id, string code, string title, string description, UserId userId, DateTime utcNow) : base(id)
    {

        Code = code;
        Title = title;
        Description = description;
        this.userId = userId;
        this.utcNow = utcNow;
    }
    public Class() { }
    public string Code { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; }
    public UserId CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    
    // Navigation
    public User Creator { get; set; }
    public ICollection<CourseClass> CourseClasses { get; set; } = new List<CourseClass>();
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();


    public static Class Create(string code, string title, string description, UserId userId, DateTime utcNow)
    {

        var cls= new Class(
            ClassId.New(),
            code,
            title,
            description,
            userId,
            utcNow);

        cls.RaiseDomainEvent(new ClassCreatedDomainEvent(cls.Id));
        return cls;
    }
}
