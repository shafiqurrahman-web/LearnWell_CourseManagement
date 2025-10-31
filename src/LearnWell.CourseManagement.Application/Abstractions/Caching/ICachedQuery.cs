using LearnWell.CourseManagement.Application.Abstractions.Messaging;

namespace LearnWell.CourseManagement.Application.Abstractions.Caching;

internal interface ICachedQuery<TResponse> : IQuery<TResponse>, ICachedQuery;

internal interface ICachedQuery
{
    string CacheKey { get; }

    TimeSpan? Expiration { get; }
        
}