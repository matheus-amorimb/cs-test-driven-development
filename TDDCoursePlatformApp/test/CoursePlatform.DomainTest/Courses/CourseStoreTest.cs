using Bogus;
using Bogus.DataSets;
using CoursePlataform.Domain.Courses;
using CoursePlataform.DomainTest.Builders;
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

    [Fact]
    public void MustNotAddACourseWithNameAlreadyAdded()
    {
        var courseAlreadySaved = CourseBuilder.New().WithName(_courseDto.Name).Build();
        _courseRepositoryMock.Setup(r => r.GetByName(_courseDto.Name)).Returns(courseAlreadySaved);
        
        Action action = () => _courseStorage.Add(_courseDto);
        action.Should().Throw<ArgumentException>().WithMessage("Course name already in use.");
    }
}