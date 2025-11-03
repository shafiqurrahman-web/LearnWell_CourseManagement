namespace LearnWell.CourseManagement.Domain.Entities.Users;
public sealed class Role
{
    public static readonly Role Staff = new(1, "Staff");
    public static readonly Role Student = new(2, "Student");

    public Role(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; init; }
    public string Name { get; init; }

    public ICollection<User> Users { get; init; } = new List<User>();
    public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
