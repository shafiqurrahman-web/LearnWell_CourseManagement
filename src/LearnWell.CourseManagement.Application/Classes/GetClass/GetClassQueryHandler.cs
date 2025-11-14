using LearnWell.CourseManagement.Application.Abstractions.Data;
using LearnWell.CourseManagement.Application.Abstractions.Messaging;
using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.Classes;
using LearnWell.CourseManagement.Domain.Entities.Courses;

namespace LearnWell.CourseManagement.Application.Classes.GetClass;

public sealed class GetClassQueryHandler : IQueryHandler<GetClassQuery, ClassResponse>
{
    private readonly IClassRepository _classRepository;

    public GetClassQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IClassRepository classRepository)
    {
        _classRepository = classRepository;

    }

    public async Task<Result<ClassResponse>> Handle(GetClassQuery request, CancellationToken cancellationToken)
    {
        var course = await _classRepository.GetByIdAsync(new ClassId(request.ClassId), cancellationToken);


        if (course is null)
            return Result.Failure<ClassResponse>(CourseErrors.NotFound);
        

        var response = new ClassResponse
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
