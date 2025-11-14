namespace LearnWell.CourseManagement.Api.Controllers.Classes;

public sealed record CreateClassRequest(
    
    string Code,
    string Title,
    string Description,
    Guid CreatedBy);

