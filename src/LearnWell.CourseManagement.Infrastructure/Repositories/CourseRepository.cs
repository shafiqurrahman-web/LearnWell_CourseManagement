using LearnWell.CourseManagement.Domain.Entities.Classes;
using LearnWell.CourseManagement.Domain.Entities.Courses;
using Microsoft.EntityFrameworkCore;

namespace LearnWell.CourseManagement.Infrastructure.Repositories;
internal sealed class CourseRepository : Repository<Course, CourseId>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsOverlappingAsync(Class clas, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Course>()
            .AnyAsync(course =>
                        course.CourseClasses.FirstOrDefault().ClassId == clas.Id,
                       cancellationToken);
    }
}
