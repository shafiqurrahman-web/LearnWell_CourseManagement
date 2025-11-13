using Asp.Versioning;
using LearnWell.CourseManagement.Application.Users.GetLoggedInUser;
using LearnWell.CourseManagement.Application.Users.LogInUser;
using LearnWell.CourseManagement.Application.Users.RegisterUser;
using LearnWell.CourseManagement.Infrastructure.Authorization.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnWell.CourseManagement.Api.Controllers.Users;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender) => _sender = sender; 

    [HttpGet("me")]
    [MapToApiVersion(ApiVersions.V1)]
    [Authorize(Policy = Policies.CanReadUser)]
    public async Task<IActionResult> GetLoggedInUserV1(CancellationToken cancellationToken)
    {
        var query = new GetLoggedInUserQuery();

        var result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpGet("me")]
    [MapToApiVersion(ApiVersions.V2)]
    [Authorize(Policy = Policies.CanReadUser)]
    public async Task<IActionResult> GetLoggedInUserV2(CancellationToken cancellationToken)
    {
        var query = new GetLoggedInUserQuery();

        var result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            request.Email,
            request.FirstName,
            request.LastName,
            request.Password);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LogInUserRequest request, CancellationToken cancellationToken)
    {
        var command = new LogInUserCommand(
            request.Email,
            request.Password);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
