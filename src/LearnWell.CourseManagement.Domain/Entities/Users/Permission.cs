namespace LearnWell.CourseManagement.Domain.Entities.Users;
public sealed class Permission
{
    public static readonly Permission CourseRead = new(1, "course:read");

    public Permission(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
}
