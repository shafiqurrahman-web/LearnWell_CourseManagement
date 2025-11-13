using Asp.Versioning;
using LearnWell.CourseManagement.Application.Courses.GenerateCourse;
using LearnWell.CourseManagement.Application.Courses.GetCourse;
using LearnWell.CourseManagement.Infrastructure.Authorization.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnWell.CourseManagement.Api.Controllers.Courses;


[Authorize]
[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/[controller]")]
public class CourseController : ControllerBase
{

    private readonly ISender _sender;
   public CourseController(ISender sender)
    {
        _sender = sender;
    }

    //GET /api/course/{id}
    [HttpGet("{id:guid}")]
    [Authorize(Policy = Policies.CanReadCourse)]
    public async Task<IActionResult> GetCourse(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCourseQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }


    //POST /api/course
    [HttpPost]
    [Authorize(Policy = Policies.CanCreateCourse)]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest request, CancellationToken cancellationToken)
    {   var command = new CreateCourseCommand(
            
            request.Code,
            request.Title,
            request.Description,
            request.CreatedBy);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetCourse), new { id = result.Value }, result.Value);
    }
}
