namespace LearnWell.CourseManagement.Api.Controllers.Classes
{
    public sealed record UpdateClassRequest(
    Guid Id,
    string Code,
    string Title,
    string Description,
    Guid UpdatedBy);
}
