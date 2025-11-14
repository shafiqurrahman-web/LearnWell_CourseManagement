using Asp.Versioning;
using LearnWell.CourseManagement.Application.Courses.DeleteCourse;
using LearnWell.CourseManagement.Application.Courses.GenerateCourse;
using LearnWell.CourseManagement.Application.Courses.GetCourse;
using LearnWell.CourseManagement.Application.Courses.GetStudentsByCourse;
using LearnWell.CourseManagement.Application.Courses.UpdateCourse;
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

    //GET /api/v1/course/{id}
    [HttpGet("{id:guid}")]
    [Authorize(Policy = Policies.CanReadCourse)]
    public async Task<IActionResult> GetCourse(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCourseQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }


    //POST /api/v1/course
    [HttpPost]
    [Authorize(Policy = Policies.CanCreateCourse)]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCourseCommand(

            request.Code,
            request.Title,
            request.Description,
            request.CreatedBy);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetCourse), new { id = result.Value }, result.Value);
    }


    // PUT /api/v1/courses/{id}
    [HttpPut("{id:guid}")]
    [Authorize(Policy = Policies.CanUpdateCourse)]
    public async Task<IActionResult> UpdateCourse(Guid id, [FromBody] UpdateCourseRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateCourseCommand(id,
            request.Code,
            request.Title, request.Description, request.UpdatedBy);
        var result = await _sender.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    // DELETE /api/v1/courses/{id}
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = Policies.CanDeleteCourse)]
    public async Task<IActionResult> DeleteCourse(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCourseCommand(id);
        var result = await _sender.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    // GET /api/v1/courses/{id}/students
    [HttpGet("{id:guid}/students")]
    [Authorize(Policy = Policies.CanReadCourse)]
    public async Task<IActionResult> GetStudentsByCourseId(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetStudentsByCourseQuery(id);
        var result = await _sender.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }


}
