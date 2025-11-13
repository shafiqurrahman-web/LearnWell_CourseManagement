namespace LearnWell.CourseManagement.Api.Controllers.Courses;

public sealed record CreateCourseRequest(
    
    string Code,
    string Title,
    string Description,
    Guid CreatedBy);

