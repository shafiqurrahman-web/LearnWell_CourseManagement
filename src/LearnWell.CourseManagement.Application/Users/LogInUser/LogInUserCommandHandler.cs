using LearnWell.CourseManagement.Application.Abstractions.Authentication;
using LearnWell.CourseManagement.Application.Abstractions.Messaging;
using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.Users;

namespace LearnWell.CourseManagement.Application.Users.LogInUser;
internal sealed class LogInUserCommandHandler : ICommandHandler<LogInUserCommand, AccessTokenResponse>
{
    private readonly IJwtService _jwtService;

    public LogInUserCommandHandler(IJwtService jwtService) => _jwtService = jwtService;

    public async Task<Result<AccessTokenResponse>> Handle(LogInUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _jwtService.GetAccessTokenAsync(
            request.Email,
            request.Password, 
            cancellationToken);

        if (result.IsFailure) return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);

        return new AccessTokenResponse(result.Value);
    }
}
