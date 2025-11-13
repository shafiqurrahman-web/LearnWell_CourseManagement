using LearnWell.CourseManagement.Domain.Entities.Classes;
using LearnWell.CourseManagement.Domain.Entities.Courses;
using LearnWell.CourseManagement.Infrastructure.Database;

namespace LearnWell.CourseManagement.Infrastructure.Repositories;
internal sealed class CourseRepository : Repository<Course, CourseId>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> IsOverlappingAsync(Class clas, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
