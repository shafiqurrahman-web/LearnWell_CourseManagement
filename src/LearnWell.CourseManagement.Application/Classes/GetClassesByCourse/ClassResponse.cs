using LearnWell.CourseManagement.Domain.Entities.Students;
using LearnWell.CourseManagement.Domain.Entities.Users;
using LearnWell.CourseManagement.Domain.Entities.Users.ValueObjects;

namespace LearnWell.CourseManagement.Application.Classes.GetClassesByCourse;


public sealed class ClassResponse
{
    private Guid value;
    private string code;
    private string title;
    private string description;
    private DateTime createdAt;

    public ClassResponse(Guid value, string code, string title, string description, DateTime createdAt)
    {
        this.value = value;
        this.code = code;
        this.title = title;
        this.description = description;
        this.createdAt = createdAt;
    }

    public StudentId Id { get; init; }
    public string FullName { get; init; } = default!;
    public string StudentNumber { get; init; } = default!;
    public Email Email { get; init; } = default!;
    public DateTime EnrolledOn { get; init; }
    public string EnrolledBy { get; init; }
    public UserId UserId { get; set; }
}
