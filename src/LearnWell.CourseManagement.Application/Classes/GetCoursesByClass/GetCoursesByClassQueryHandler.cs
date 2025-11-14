using LearnWell.CourseManagement.Application.Abstractions.Data;
using LearnWell.CourseManagement.Application.Abstractions.Messaging;
using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.Classes;
using LearnWell.CourseManagement.Domain.Entities.Courses;


namespace LearnWell.CourseManagement.Application.Classes.GetCoursesByClass;

public sealed class GetCoursesByClassQueryHandler : IQueryHandler<GetCoursesByClassQuery, IReadOnlyList<CourseResponse>>
{
    private readonly IClassRepository _classeRepository;

    public GetCoursesByClassQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IClassRepository classeRepository)
    {
        _classeRepository = classeRepository;

    }
    
    public async Task<Result<IReadOnlyList<CourseResponse>>> Handle(
        GetCoursesByClassQuery query,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<Course> courses = await _classeRepository.GetCoursesByClassIdAsync(
            new ClassId(query.ClassId),
            cancellationToken);

        if (courses is null || courses.Count == 0)
            return Result.Failure<IReadOnlyList<CourseResponse>>(ClassErrors.NotFound);

        List <CourseResponse> courseResLst = new List<CourseResponse>();
        foreach (var crs in courses)
        {
            CourseResponse std = new CourseResponse
            {
                
                Code = crs.Code,
                Title = crs.Title,
                Description = crs.Description,
                CreatedBy = crs.CreatedBy,
                CreatedAt = crs.CreatedAt,
            };
            courseResLst.Add(std);
        }
        return Result.Success<IReadOnlyList<CourseResponse>>(courseResLst);
    }
}
