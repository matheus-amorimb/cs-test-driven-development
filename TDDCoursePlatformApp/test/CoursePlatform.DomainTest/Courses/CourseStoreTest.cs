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
        action.Should().Throw<ArgumentException>().WithMessage(Resource.TargetAudienceInvalid);
    }

    [Fact]
    public void MustNotAddACourseWithNameAlreadyAdded()
    {
        var courseAlreadySaved = CourseBuilder.New().WithName(_courseDto.Name).Build();
        _courseRepositoryMock.Setup(r => r.GetByName(_courseDto.Name)).Returns(courseAlreadySaved);
        
        Action action = () => _courseStorage.Add(_courseDto);
        action.Should().Throw<ArgumentException>().WithMessage(Resource.NameAlreadyInUse);
    }

    [Fact]
    public void MustEditCourseData()
    {
        var course = CourseBuilder.New().Build();
        var id = Guid.NewGuid();
        _courseRepositoryMock.Setup(r => r.GetById(id)).ReturnsAsync(course);

        _courseStorage.Update(id, _courseDto);
        
        course.Name.Should().Be(_courseDto.Name);
        course.Price.Should().Be(_courseDto.Price);
        course.Workload.Should().Be(_courseDto.Workload);
    }    
    
    [Fact]
    public void MustNotAddACourseWithNonexistId()
    {
        Course course = null;
        var id = Guid.NewGuid();
        _courseRepositoryMock.Setup(r => r.GetById(id)).ReturnsAsync(course);
        
        Action action = () => _courseStorage.Update(id, _courseDto);
        action.Should().Throw<ArgumentException>();
    }
    
}