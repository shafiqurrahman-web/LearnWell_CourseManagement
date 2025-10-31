using LearnWell.CourseManagement.Domain.Entities.Users;
using LearnWell.CourseManagement.Domain.Entities.Users.ValueObjects;

namespace LearnWell.CourseManagement.Application.UnitTests.Users;
internal static class UserData
{
   // public static User Create() => User.Create(FirstName, LastName, Email);

    public static readonly string FirstName = "First";
    public static readonly string LastName = "Last";
    public static readonly Email Email = new("test@test.com");
}
