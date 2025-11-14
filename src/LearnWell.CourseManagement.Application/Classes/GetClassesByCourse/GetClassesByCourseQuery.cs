using LearnWell.CourseManagement.Application.Abstractions.Messaging;

namespace LearnWell.CourseManagement.Application.Classes.GetClassesByCourse;

public record GetClassesByCourseQuery(Guid CourseId):IQuery<IReadOnlyList<ClassResponse>>;

