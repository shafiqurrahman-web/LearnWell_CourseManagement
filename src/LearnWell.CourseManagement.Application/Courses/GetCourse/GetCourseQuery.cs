using LearnWell.CourseManagement.Application.Abstractions.Caching;

namespace LearnWell.CourseManagement.Application.Courses.GetCourse;
public record GetCourseQuery(Guid CourseId) : ICachedQuery<CourseResponse>
{
    public string CacheKey => $"courses-{CourseId}";

    public TimeSpan? Expiration => null;
    
}
