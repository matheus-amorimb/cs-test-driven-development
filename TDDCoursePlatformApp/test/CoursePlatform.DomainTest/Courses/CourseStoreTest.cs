using Bogus;
using Bogus.DataSets;
using CoursePlataform.Domain.Courses;
using CoursePlataform.Domain.Utilities;
using CoursePlataform.DomainTest.Builders;
using FluentAssertions;
using Moq;

namespace CoursePlataform.DomainTest.Courses;

public class CourseStoreTest
{
    private readonly CourseDto _courseDto;
    private readonly CourseEditDto _courseEditDto;
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
        _courseEditDto = new CourseEditDto()
        {
            Id = fake.Random.Guid(),
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
    public void Add_CourseSuccessfully()
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
    public void Add_InvalidTargetAudience_ThrowsArgumentException()
    {
        _courseDto.TargetAudience = "Doctor";

        Action action = () => _courseStorage.Add(_courseDto);
        action.Should().Throw<ArgumentException>().WithMessage(Resource.TargetAudienceInvalid);
    }

    [Fact]
    public void Add_DuplicateCourseName_ThrowsArgumentException()
    {
        var courseAlreadySaved = CourseBuilder.New().WithName(_courseDto.Name).Build();
        _courseRepositoryMock.Setup(r => r.GetByName(_courseDto.Name)).Returns(courseAlreadySaved);
        
        Action action = () => _courseStorage.Add(_courseDto);
        action.Should().Throw<ArgumentException>().WithMessage(Resource.NameAlreadyInUse);
    }

    [Fact]
    public async void Update_CourseSuccessfully()
    {
        var course = CourseBuilder.New().Build();
        var id = Guid.NewGuid();
        _courseRepositoryMock.Setup(r => r.GetById(id)).ReturnsAsync(course);

        await _courseStorage.Update(id, _courseDto);
        
        course.Name.Should().Be(_courseDto.Name);
        course.Price.Should().Be(_courseDto.Price);
        course.Workload.Should().Be(_courseDto.Workload);
    }    
    
    [Fact]
    public async void Update_NonexistentCourseId_ThrowsArgumentException()
    {
        Course course = null;
        var id = Guid.NewGuid();
        _courseRepositoryMock.Setup(r => r.GetById(id)).ReturnsAsync(course);
     
        Func<Task> action = async () => await _courseStorage.Update(id, _courseDto);
        await action.Should().ThrowAsync<ArgumentException>();
    }
    
}