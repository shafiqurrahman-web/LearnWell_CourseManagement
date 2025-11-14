using LearnWell.CourseManagement.Application.Abstractions.Data;
using LearnWell.CourseManagement.Application.Abstractions.Messaging;
using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.Classes;
using LearnWell.CourseManagement.Domain.Entities.Courses;
using LearnWell.CourseManagement.Domain.Entities.Students;

namespace LearnWell.CourseManagement.Application.Classes.GetClassesByCourse;

public sealed class GetClassesByCourseQueryHandler : IQueryHandler<GetClassesByCourseQuery, IReadOnlyList<ClassResponse>>
{
    private readonly IClassRepository _classeRepository;

    public GetClassesByCourseQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IClassRepository classeRepository)
    {
        _classeRepository = classeRepository;

    }

    public async Task<Result<IReadOnlyList<ClassResponse>>> Handle(
        GetClassesByCourseQuery query,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<Class> classes = await _classeRepository.GetClassesByCourseIdAsync(
            new CourseId(query.CourseId),
            cancellationToken);

        if (classes is null || classes.Count == 0)
            return Result.Failure<IReadOnlyList<ClassResponse>>(CourseErrors.NotFound);

        var response = classes
           .Select(c => new ClassResponse(
               c.Id.Value,
               c.Code,
               c.Title,
               c.Description,
               c.CreatedAt
           ))
           .ToList();

        return Result.Success<IReadOnlyList<ClassResponse>>(response);
        //return Result.Success<IReadOnlyList<ClassResponse>>(studentsResLst);
    }
}
