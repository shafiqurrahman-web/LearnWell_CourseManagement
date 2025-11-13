using LearnWell.CourseManagement.Application.Abstractions.Messaging;

namespace LearnWell.CourseManagement.Application.Users.RegisterUser;
public sealed record RegisterUserCommand(
    string Email,
    string FirstName,
    string LastName,
    string Password) : ICommand<Guid>;
