using LearnWell.CourseManagement.Domain.Entities.Abstractions;

namespace LearnWell.CourseManagement.Domain.Entities.Classes.Events;

public sealed record ClassCreatedDomainEvent(ClassId CourseId) : IDomainEvent;
