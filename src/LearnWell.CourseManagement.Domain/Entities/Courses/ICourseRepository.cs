using LearnWell.CourseManagement.Domain.Entities.Classes;

namespace LearnWell.CourseManagement.Domain.Entities.Courses;

public interface ICourseRepository
{
    Task<Course> GetByIdAsync(CourseId id, CancellationToken cancellationToken = default);

    Task<bool> IsOverlappingAsync(
        Class clas,        
        CancellationToken cancellationToken = default);

    void Add(Course course);
}
