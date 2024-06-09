using Bogus;
using Bogus.DataSets;
using CoursePlataform.Domain.Courses;
using FluentAssertions;
using Moq;

namespace CoursePlataform.DomainTest.Courses;

public class CourseStoreTest
{
    private readonly CourseDto _courseDto;
    private readonly CourseStorage _courseStorage;
    private readonly Mock<ICourseRepository> _courseRepositoryMock;

    public CourseStoreTest()
    {
        var fake = new Faker();
        _courseDto = new CourseDto
        {
            Name = fake.Random.Words(),
            Description = fake.Lorem.Paragraph(),
            Workload = fake.Random.Double(1, 50),
            TargetAudience = "Employee",
            Price = fake.Random.Double(1, 12000),
        };
        _courseRepositoryMock = new Mock<ICourseRepository>();
        _courseStorage = new CourseStorage(_courseRepositoryMock.Object);
    }

    [Fact]
    public void MustAddCourse()
    {
        _courseStorage.Add(_courseDto);
        
        _courseRepositoryMock.Verify(r => r.Add(
            It.Is<Course>(
                c => c.Name == _courseDto.Name &&
                     c.Description == _courseDto.Description
                )
            ), Times.AtLeast(1));
    }

    [Fact]
    public void MustInformAValidTargetAudience()
    {
        _courseDto.TargetAudience = "Doctor";

        Action action = () => _courseStorage.Add(_courseDto);
        action.Should().Throw<ArgumentException>().WithMessage("Target audience invalid.");
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
        bool tryParse = Enum.TryParse<TargetAudience>(courseDto.TargetAudience, out var targetAudience);

        if (!tryParse) throw new ArgumentException("Target audience invalid.");

        var course = new Course(courseDto.Name, courseDto.Workload, targetAudience, courseDto.Price,
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
    public string? TargetAudience { get; set; }
    public double Price { get; set; }
}