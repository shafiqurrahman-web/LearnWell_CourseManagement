using LearnWell.CourseManagement.Application.Abstractions.Data;
using LearnWell.CourseManagement.Application.Abstractions.Messaging;
using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.Courses;
using LearnWell.CourseManagement.Domain.Entities.Students;

namespace LearnWell.CourseManagement.Application.Courses.GetStudentsByCourse;

public sealed class GetStudentsByCourseQueryHandler : IQueryHandler<GetStudentsByCourseQuery, IReadOnlyList<StudentResponse>>
{
    private readonly ICourseRepository _courseRepository;

    public GetStudentsByCourseQueryHandler(ISqlConnectionFactory sqlConnectionFactory, ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;

    }
    
    public async Task<Result<IReadOnlyList<StudentResponse>>> Handle(
        GetStudentsByCourseQuery query,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<Student> students = await _courseRepository.GetStudentsByCourseIdAsync(
            new CourseId(query.CourseId),
            cancellationToken);

        if (students is null || students.Count == 0)
            return Result.Failure<IReadOnlyList<StudentResponse>>(CourseErrors.NotFound);

        List <StudentResponse> studentsResLst = new List<StudentResponse>();
        foreach (var student in students)
        {
            StudentResponse std = new StudentResponse
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.User.Email,
                EnrolledOn = student.CreatedAt,
                EnrolledBy = student.User.FirstName
            };
            studentsResLst.Add(std);
        }
        return Result.Success<IReadOnlyList<StudentResponse>>(studentsResLst);
    }
}
