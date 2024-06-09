using Bogus.DataSets;
using CoursePlataform.Domain.Courses;
using Moq;

namespace CoursePlataform.DomainTest.Courses;

public class CourseStoreTest
{
    [Fact]
    public void MustAddCourse()
    {
        var courseDto = new CourseDto
        {
            Name = "Clean Architecture",
            Description = "How to create a Web Api using Clean Architecture",
            Workload = 24,
            TargetAudience = TargetAudience.Employee,
            Price = 1299,
        };

        var courseRepositoryMock = new Mock<ICourseRepository>();
        
        var courseStorage = new CourseStorage(courseRepositoryMock.Object);

        courseStorage.Add(courseDto);
        
        courseRepositoryMock.Verify(r => r.Add(It.Is<Course>(c => c.Name == courseDto.Name)));
    }
}
 
public class CourseStorage
{
    private readonly ICourseRepository _courseRepository;

    public CourseStorage(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public void Add(CourseDto courseDto)
    {
        var course = new Course(courseDto.Name, courseDto.Workload, courseDto.TargetAudience, courseDto.Price,
            courseDto.Description);
        _courseRepository.Add(course);
    }
} 

public interface ICourseRepository
{
    void Add(Course course);
}

public class CourseDto
{    
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Workload { get; set; }
    public TargetAudience TargetAudience { get; set; }
    public double Price { get; set; }
}