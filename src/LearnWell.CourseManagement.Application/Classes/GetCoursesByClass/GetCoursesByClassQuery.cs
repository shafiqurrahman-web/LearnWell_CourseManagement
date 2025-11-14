using LearnWell.CourseManagement.Application.Abstractions.Messaging;

namespace LearnWell.CourseManagement.Application.Classes.GetCoursesByClass;

public record GetCoursesByClassQuery(Guid ClassId):IQuery<IReadOnlyList<CourseResponse>>;

