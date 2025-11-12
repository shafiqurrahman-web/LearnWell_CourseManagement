using LearnWell.CourseManagement.Domain.Entities.Users;
using LearnWell.CourseManagement.Infrastructure.Ddatabase;

namespace LearnWell.CourseManagement.Infrastructure.Repositories;
internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public override void Add(User user)
    {        
        foreach (var role in user.Roles)
        {
            DbContext.Attach(role);
        }

        DbContext.Add(user);
    }
}
