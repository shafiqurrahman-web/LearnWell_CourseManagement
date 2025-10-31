using Microsoft.AspNetCore.Mvc;
using MediatR;
using LearnWell.CourseManagement.Application.Courses.GetCourse;

namespace LearnWell.CourseManagement.Api.Controllers.Courses;

//[Route("api/v{version:apiVersion}/course")]
[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{

    private readonly ISender _sender;
   public CourseController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourse(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCourseQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }
}
