using LearnWell.CourseManagement.Application.Abstractions.Caching;
using LearnWell.CourseManagement.Domain.Entities.Courses;

namespace LearnWell.CourseManagement.Application.Courses.GetCourse;
public record GetCourseQuery(Guid CourseId) : ICachedQuery<Course>
{
    public string CacheKey => $"courses-{CourseId}";

    public TimeSpan? Expiration => null;
    
}
