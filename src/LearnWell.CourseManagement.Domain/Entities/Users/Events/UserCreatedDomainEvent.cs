using LearnWell.CourseManagement.Domain.Entities.Abstractions;

namespace LearnWell.CourseManagement.Domain.Entities.Users.Events;

public sealed record UserCreatedDomainEvent(UserId UserId) : IDomainEvent;
