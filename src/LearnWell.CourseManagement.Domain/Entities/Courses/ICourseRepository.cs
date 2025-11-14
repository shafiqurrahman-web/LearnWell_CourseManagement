using LearnWell.CourseManagement.Domain.Entities.Classes;
using LearnWell.CourseManagement.Domain.Entities.Students;

namespace LearnWell.CourseManagement.Domain.Entities.Courses;

public interface ICourseRepository
{
    Task<Course> GetByIdAsync(CourseId id, CancellationToken cancellationToken = default);

    Task<bool> IsOverlappingAsync(
        Class clas,        
        CancellationToken cancellationToken = default);

    void Add(Course course);
    void Update(Course course);
    Task DeleteByIdAsync(CourseId courseId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Student>> GetStudentsByCourseIdAsync(
        CourseId courseId,
        CancellationToken cancellationToken = default);


}
