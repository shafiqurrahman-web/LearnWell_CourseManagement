using LearnWell.CourseManagement.Domain.Entities.Users.ValueObjects;

namespace LearnWell.CourseManagement.Domain.UnitTests.Users;
internal static class UserData
{
    public static readonly string FirstName = "First";
    public static readonly string LastName = "Last";
    public static readonly Email Email = new("test@test.com");    
}
