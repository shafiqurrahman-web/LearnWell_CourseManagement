using LearnWell.CourseManagement.Domain.Entities.Classes;
using LearnWell.CourseManagement.Domain.Entities.Courses;
using LearnWell.CourseManagement.Domain.Entities.Students;

namespace LearnWell.CourseManagement.Domain.Entities.Classes;

public interface IClassRepository
{
    Task<Class> GetByIdAsync(ClassId id, CancellationToken cancellationToken = default);

    void Add(Class course);
    void Update(Class course);
    Task DeleteByIdAsync(ClassId courseId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Class>> GetClassesByCourseIdAsync(
        CourseId classId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Course>> GetCoursesByClassIdAsync(
        ClassId classId,
        CancellationToken cancellationToken = default);

    


}
