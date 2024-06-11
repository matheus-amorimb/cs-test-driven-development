using Bogus;
using CoursePlataform.Domain.Courses;
using CoursePlataform.DomainTest.Builders;
using CoursePlataform.DomainTest.Utilities;
using FluentAssertions;
using Xunit.Abstractions;

namespace CoursePlataform.DomainTest.Courses;

public class CourseTest : IDisposable
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly string _name;
    private readonly double _workload;
    private readonly TargetAudience _targetAudience;
    private readonly double _price;
    private readonly string _description;
    public CourseTest(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _outputHelper.WriteLine("Constructor being executed...");
        
        var faker = new Faker();
        _name = faker.Random.Word();
        _workload = faker.Random.Double(50, 100);
        _targetAudience = TargetAudience.Employee;
        _price = faker.Random.Double(100, 1800);
        _description = faker.Lorem.Paragraph();
    }
    
    public void Dispose()
    {
        _outputHelper.WriteLine("Disposable being executed...");
    }

    [Fact]
    public void MustCreateCourse()
    {
        var expectedCourse = new
        {
            Name = _name,
            Workload = _workload,
            TargetAudience = _targetAudience,
            Price = _price,
            Description = _description,
        };
        
        var course = new Course(expectedCourse.Name, expectedCourse.Workload, expectedCourse.TargetAudience, expectedCourse.Price, expectedCourse.Description);
        
        course.Should().BeEquivalentTo(expectedCourse);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void CourseMustNotHaveAInvalidName(string? invalidName)
    {
        Action action = () => CourseBuilder.New().WithName(invalidName).Build();
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    [InlineData(-100)]
    public void CourseMustHaveAtLeastOneHourLength(double invalidWorkload)
    {
        Assert.Throws<ArgumentException>(() => CourseBuilder.New().WithWorkload(invalidWorkload).Build()).WithMessage("Course must have at least one hour length");
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void CourseMustNotHaveAPriceLowerThan1(double invalidPrice)
    {
        Action action = () => CourseBuilder.New().WithPrice(invalidPrice).Build();
        action.Should().Throw<ArgumentException>().WithMessage("Course price must be greater than 1");
    }

    [Fact]
    public void MustChangeName()
    {
        var expectedName = "Pedro";
        var course = CourseBuilder.New().Build();

        course.ChangeName(expectedName);

        course.Name.Should().Be(expectedName);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void CourseNameMustNotBeChangedWithAnInvalidName(string invalidName)
    {
        Action action = () => CourseBuilder.New().Build().ChangeName(invalidName);
        action.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void MustChangeWorkLoad()
    {
        var expectedWorkLoad = 55;
        var course = CourseBuilder.New().Build();

        course.ChangeWorkLoad(expectedWorkLoad);

        course.Workload.Should().Be(expectedWorkLoad);
    }
    
    
}