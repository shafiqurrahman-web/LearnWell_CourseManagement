using LearnWell.CourseManagement.Domain.Entities.Classes;
using LearnWell.CourseManagement.Domain.Entities.CourseClasses;
using LearnWell.CourseManagement.Domain.Entities.Courses;
using LearnWell.CourseManagement.Domain.Entities.Students;
using LearnWell.CourseManagement.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IReadOnlyList<Student>> GetStudentsByCourseIdAsync(
        CourseId courseId,
        CancellationToken cancellationToken = default)
    {

        var exists = await DbContext.Set<Course>()
          .AnyAsync(c => c.Id == courseId, cancellationToken);

        if (!exists)
            return [];

        var students = await DbContext
                .Set<CourseClass>()
                .Where(cc => cc.CourseId == courseId)
                .SelectMany(cc => cc.Class.Enrollments)
                .Where(e => e.Student != null)
                .Select(e => new Student
                {
                    Id = e.Student.Id,
                    FullName = e.Student.FullName,
                    StudentNumber = e.Student.StudentNumber,
                    UserId = e.Student.UserId,
                    CreatedAt = e.Student.CreatedAt
                })
                .Distinct()
                .ToListAsync(cancellationToken);

        return students;
    }


}

