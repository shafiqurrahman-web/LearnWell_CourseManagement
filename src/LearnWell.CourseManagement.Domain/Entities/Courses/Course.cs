using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.CourseClasses;
using LearnWell.CourseManagement.Domain.Entities.Courses.Events;
using LearnWell.CourseManagement.Domain.Entities.StudentCourses;
using LearnWell.CourseManagement.Domain.Entities.Users;

namespace LearnWell.CourseManagement.Domain.Entities.Courses;

public class Course : Entity<CourseId>
{

    public Course(CourseId id, string code, string title, string description, UserId userId, DateTime utcNow) : base(id)
    {
        Code = code;
        Title = title;
        Description = description;
        CreatedBy = userId;
        CreatedAt = utcNow;
    }
    public Course() { }
    public string Code { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; }
    public UserId CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public User Creator { get; set; }
    public ICollection<CourseClass> CourseClasses { get; set; } = new List<CourseClass>();
    public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();




    public static Course Create(string code, string title, string description, UserId userId, DateTime utcNow)
    {

        var course = new Course(
            CourseId.New(),
            code,
            title,
            description,
            userId,
            utcNow);

        course.RaiseDomainEvent(new CourseCreatedDomainEvent(course.Id));
        return course;
    }
}
