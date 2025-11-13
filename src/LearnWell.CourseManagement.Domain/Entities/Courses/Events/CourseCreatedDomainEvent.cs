using LearnWell.CourseManagement.Domain.Entities.Abstractions;

namespace LearnWell.CourseManagement.Domain.Entities.Courses.Events;

public sealed record CourseCreatedDomainEvent(CourseId CourseId) : IDomainEvent;
