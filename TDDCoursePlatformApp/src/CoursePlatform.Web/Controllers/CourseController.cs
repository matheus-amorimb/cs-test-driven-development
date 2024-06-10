using CoursePlataform.DomainTest.Courses;
using Microsoft.AspNetCore.Mvc;

namespace CoursePlatform.Web.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly CourseStorage _courseStorage;

    public CourseController(CourseStorage courseStorage)
    {
        _courseStorage = courseStorage;
    }

    [HttpPost]
    [Route("create")]
    public ActionResult<CourseDto> Create(CourseDto courseDto)
    {
        _courseStorage.Add(courseDto);
        
        return Ok(courseDto);
    }
}