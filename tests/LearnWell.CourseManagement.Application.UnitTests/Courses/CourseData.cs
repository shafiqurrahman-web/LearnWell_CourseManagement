using LearnWell.CourseManagement.Domain.Entities.Courses;
using LearnWell.CourseManagement.Domain.Entities.Users;

namespace LearnWell.CourseManagement.Application.UnitTests.Courses;
internal static class CourseData
{
    public static Course Create() => new(
        CourseId.New(),
        "Test Course",
        "TC101",
        "A course for testing purposes",
        UserId.New(),
        DateTime.UtcNow);


}
