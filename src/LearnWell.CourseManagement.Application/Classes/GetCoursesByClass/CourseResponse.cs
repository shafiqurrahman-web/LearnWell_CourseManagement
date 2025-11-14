using LearnWell.CourseManagement.Domain.Entities.Courses;
using LearnWell.CourseManagement.Domain.Entities.Students;
using LearnWell.CourseManagement.Domain.Entities.Users;
using LearnWell.CourseManagement.Domain.Entities.Users.ValueObjects;

namespace LearnWell.CourseManagement.Application.Classes.GetCoursesByClass;


public sealed class CourseResponse
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; }
    public UserId CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}
