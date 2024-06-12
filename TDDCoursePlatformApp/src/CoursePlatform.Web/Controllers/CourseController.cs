using CoursePlataform.Domain.Courses;
using CoursePlataform.DomainTest.Courses;
using CoursePlatform.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoursePlatform.Web.Controllers;

[ApiController]
[Route("/api/course")]
public class CourseController : ControllerBase
{
    private readonly CourseStorage _courseStorage;
    private readonly IRepository<Course> _repository;

    public CourseController(CourseStorage courseStorage, IRepository<Course> repository)
    {
        _courseStorage = courseStorage;
        _repository = repository;
    }

    [HttpPost]
    [Route("create")]
    public ActionResult<CourseDto> Create(CourseDto courseDto)
    {
        _courseStorage.Add(courseDto);
        
        return Ok(courseDto);
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll()
    {
        var courses = await _repository.GetAll();

        var coursesDto = courses.Select(c => new CourseDto()
        {
            Name = c.Name, 
            Description = c.Description,    
            Workload = c.Workload,
            TargetAudience = nameof(c.TargetAudience), 
            Price = c.Price
        });

        return Ok(coursesDto);
    }

    [HttpPost]
    [Route("edit/{id:guid}")]
    public async Task<ActionResult<CourseEditDto>> Edit(Guid id, CourseDto courseDto)
    {
        await _courseStorage.Update(id, courseDto);

        return Ok(courseDto);
    } 
}