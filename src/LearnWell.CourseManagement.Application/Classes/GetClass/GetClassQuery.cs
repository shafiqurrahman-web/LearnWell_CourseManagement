using LearnWell.CourseManagement.Application.Abstractions.Caching;

namespace LearnWell.CourseManagement.Application.Classes.GetClass;
public record GetClassQuery(Guid ClassId) : ICachedQuery<ClassResponse>
{
    public string CacheKey => $"classes-{ClassId}";

    public TimeSpan? Expiration => null;
    
}
