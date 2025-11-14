using LearnWell.CourseManagement.Domain.Entities.Classes;
using LearnWell.CourseManagement.Domain.Entities.CourseClasses;
using LearnWell.CourseManagement.Domain.Entities.Courses;
using LearnWell.CourseManagement.Domain.Entities.Students;
using LearnWell.CourseManagement.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LearnWell.CourseManagement.Infrastructure.Repositories;
internal sealed class ClassRepository : Repository<Class, ClassId>, IClassRepository
{


    public ClassRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<IReadOnlyList<Class>> GetClassesByCourseIdAsync(CourseId courseId, CancellationToken cancellationToken = default)
    {
        var exists = await DbContext.Set<Course>()
         .AnyAsync(c => c.Id == courseId, cancellationToken);

        if (!exists)
            return Array.Empty<Class>();

        var classes = await DbContext
                .Set<CourseClass>()
                .Where(cc => cc.CourseId == courseId)
                .Select(cc => cc.Class)
                .Distinct()
                .ToListAsync(cancellationToken);

        return classes;
    }

    public async Task<IReadOnlyList<Course>> GetCoursesByClassIdAsync(
    ClassId classId,
    CancellationToken cancellationToken = default)
    {
        var courseIds = await DbContext.Set<CourseClass>()
            .Where(cc => cc.ClassId == classId)
            .Select(cc => cc.CourseId)
            .Distinct()
            .ToListAsync(cancellationToken);

        if (!courseIds.Any())
            return Array.Empty<Course>();

        var courses = await DbContext.Set<Course>()
            .Where(c => courseIds.Contains(c.Id))
            .ToListAsync(cancellationToken);

        return courses;
    }


}

