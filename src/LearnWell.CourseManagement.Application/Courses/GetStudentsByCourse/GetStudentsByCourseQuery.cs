using LearnWell.CourseManagement.Application.Abstractions.Messaging;

namespace LearnWell.CourseManagement.Application.Courses.GetStudentsByCourse;

public record GetStudentsByCourseQuery(Guid CourseId):IQuery<IReadOnlyList<StudentResponse>>;

