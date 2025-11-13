using LearnWell.CourseManagement.Application.Abstractions.Messaging;

namespace LearnWell.CourseManagement.Application.Users.LogInUser;
public sealed record LogInUserCommand(string Email, string Password) : ICommand<AccessTokenResponse>;