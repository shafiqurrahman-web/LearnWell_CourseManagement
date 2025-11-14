

using Asp.Versioning;
using LearnWell.CourseManagement.Application.Classes.DeleteClass;
using LearnWell.CourseManagement.Application.Classes.GenerateClass;
using LearnWell.CourseManagement.Application.Classes.GetClass;
using LearnWell.CourseManagement.Application.Classes.GetClassesByCourse;
using LearnWell.CourseManagement.Application.Classes.GetCoursesByClass;
using LearnWell.CourseManagement.Application.Classes.UpdateClass;
using LearnWell.CourseManagement.Application.Courses.GetCourse;
using LearnWell.CourseManagement.Infrastructure.Authorization.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnWell.CourseManagement.Api.Controllers.Classes;

[Authorize]
[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/[controller]")]
public class ClassController : ControllerBase
{
    private readonly ISender _sender;

    public ClassController(ISender sender)
    {
        _sender = sender;
    }

    //GET: /api/v1/class/{id}
    [HttpGet("{id:guid}")]
    [Authorize(Policy = Policies.CanReadClass)]
    public async Task<IActionResult> GetClass(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetClassQuery(id);
        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }


    //POST: /api/v1/class
    [HttpPost]
    [Authorize(Policy = Policies.CanCreateClass)]
    public async Task<IActionResult> CreateClass([FromBody] CreateClassRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateClassCommand(
            request.Code,
            request.Title,
            request.Description,
            request.CreatedBy);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetClass), new { id = result.Value }, result.Value);
    }

    // PUT: /api/v1/class/{id}
    [HttpPut("{id:guid}")]
    [Authorize(Policy = Policies.CanUpdateClass)]
    public async Task<IActionResult> UpdateClass(Guid id, [FromBody] UpdateClassRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateClassCommand(id,
            request.Code, request.Title, request.Description, request.UpdatedBy);

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    

    //DELETE: /api/v1/class/{id}
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = Policies.CanDeleteClass)]
    public async Task<IActionResult> DeleteClass(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteClassCommand(id);
        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : NotFound(result.Error);
    }

    //GET: /api/v1/class/{classId}/courses
    [HttpGet("{classId:guid}/courses")]
    [Authorize(Policy = Policies.CanReadClass)]
    public async Task<IActionResult> GetCoursesByClass(Guid classId, CancellationToken cancellationToken)
    {
        var query = new GetCoursesByClassQuery(classId);
        var result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    // 🔹 GET: /api/v1/course/{courseId}/classes
    [HttpGet("~/api/v{version:apiVersion}/course/{courseId:guid}/classes")]
    [Authorize(Policy = Policies.CanReadCourse)]
    public async Task<IActionResult> GetClassesByCourse(Guid courseId, CancellationToken cancellationToken)
    {
        var query = new GetClassesByCourseQuery(courseId);
        var result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

        
}



