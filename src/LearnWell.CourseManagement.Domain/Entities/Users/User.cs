using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.Classes;
using LearnWell.CourseManagement.Domain.Entities.Courses;
using LearnWell.CourseManagement.Domain.Entities.Students;
using LearnWell.CourseManagement.Domain.Entities.Users.Events;
using LearnWell.CourseManagement.Domain.Entities.Users.ValueObjects;

namespace LearnWell.CourseManagement.Domain.Entities.Users;
public sealed class User : Entity<UserId>
{
    private User(UserId id, string firstName, string lastName, Email email)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
    private User() { }
    public string Username { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public Email Email { get; set; } = default!;    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string IdentityId { get; private set; } = string.Empty;

    // Navigation
    public ICollection<Role> Roles { get; set; } = new List<Role>();
    public Student StudentProfile { get; set; }
    public ICollection<Course> CreatedCourses { get; set; } = new List<Course>();
    public ICollection<Class> CreatedClasses { get; set; } = new List<Class>();


    public static User Create(string firstName, string lastName, Email email)
    {
        var user = new User(UserId.New(), firstName, lastName, email);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        user.Roles.Add(Role.Staff);

        return user;
    }

    public void SetIdentityId(string identityId) => IdentityId = identityId;

}