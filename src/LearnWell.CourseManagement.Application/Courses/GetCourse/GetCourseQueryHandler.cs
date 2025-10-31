using LearnWell.CourseManagement.Application.Abstractions.Data;
using LearnWell.CourseManagement.Application.Abstractions.Messaging;
using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.Courses;
using LearnWell.CourseManagement.Domain.Entities.Users;

namespace LearnWell.CourseManagement.Application.Courses.GetCourse;

public sealed class GetCourseQueryHandler : IQueryHandler<GetCourseQuery, Course>
{
    private readonly ICourseRepository _courseRepository;

    public GetCourseQueryHandler(ISqlConnectionFactory sqlConnectionFactory, ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;

    }

    public async Task<Result<Course>> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(new CourseId(request.CourseId), cancellationToken);

        if (course is null)
            return Result.Failure<Course>(CourseErrors.NotFound);

        return course;


    }
}
