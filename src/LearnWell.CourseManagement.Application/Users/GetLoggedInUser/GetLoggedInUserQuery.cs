using LearnWell.CourseManagement.Application.Abstractions.Messaging;

namespace LearnWell.CourseManagement.Application.Users.GetLoggedInUser;
public sealed record GetLoggedInUserQuery : IQuery<UserResponse>;
