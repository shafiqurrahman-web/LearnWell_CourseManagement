using LearnWell.CourseManagement.Domain.Entities.Students;
using LearnWell.CourseManagement.Domain.Entities.Users;
using LearnWell.CourseManagement.Domain.Entities.Users.ValueObjects;

namespace LearnWell.CourseManagement.Application.Courses.GetStudentsByCourse;

public sealed class StudentResponse
{
    public StudentId Id { get; init; }
    public string FullName { get; init; } = default!;
    public string StudentNumber { get; init; } = default!;
    public Email Email { get; init; } = default!;
    public DateTime EnrolledOn { get; init; }
    public string EnrolledBy { get; init; }
    public UserId UserId { get; set; }
}
