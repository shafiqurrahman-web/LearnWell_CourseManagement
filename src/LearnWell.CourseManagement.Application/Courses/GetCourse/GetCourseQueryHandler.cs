using LearnWell.CourseManagement.Application.Abstractions.Data;
using LearnWell.CourseManagement.Application.Abstractions.Messaging;
using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.Courses;

namespace LearnWell.CourseManagement.Application.Courses.GetCourse;

public sealed class GetCourseQueryHandler : IQueryHandler<GetCourseQuery, CourseResponse>
{
    private readonly ICourseRepository _courseRepository;

    public GetCourseQueryHandler(ISqlConnectionFactory sqlConnectionFactory, ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;

    }

    public async Task<Result<CourseResponse>> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(new CourseId(request.CourseId), cancellationToken);


        if (course is null)
            return Result.Failure<CourseResponse>(CourseErrors.NotFound);
        

        var response = new CourseResponse
        {
            Id = course.Id.Value,
            Code = course.Code,
            Title = course.Title,
            Description = course.Description,
            CreatedAt = course.CreatedAt,
            CreatedBy = course.CreatedBy            
        };

        return Result.Success(response);
    }
}
