namespace LearnWell.CourseManagement.Api.Controllers.Courses
{
    public sealed record UpdateCourseRequest(
    Guid Id,
    string Code,
    string Title,
    string Description,
    Guid UpdatedBy);
}
