using Bogus;
using CoursePlataform.Domain.Courses;
using CoursePlataform.Domain.Utilities;
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
    private readonly Faker _faker;
    public CourseTest(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _outputHelper.WriteLine("Constructor being executed...");
        
        _faker = new Faker();
        _name = _faker.Random.Word();
        _workload = _faker.Random.Double(50, 100);
        _targetAudience = TargetAudience.Employee;
        _price = _faker.Random.Double(100, 1800);
        _description = _faker.Lorem.Paragraph();
    }
    
    public void Dispose()
    {
        _outputHelper.WriteLine("Disposable being executed...");
    }

    [Fact]
    public void Constructor_ShouldCreateCourse()
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
    public void Course_InvalidName_ShouldThrowArgumentException(string? invalidName)
    {
        Action action = () => CourseBuilder.New().WithName(invalidName).Build();
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    [InlineData(-100)]
    public void Course_InvalidWorkload_ShouldThrowArgumentException(double invalidWorkload)
    {
        Assert.Throws<ArgumentException>(() => CourseBuilder.New().WithWorkload(invalidWorkload).Build()).WithMessage(Resource.InvalidWorkload);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void Course_PriceLowerThan1_ShouldThrowArgumentException(double invalidPrice)
    {
        Action action = () => CourseBuilder.New().WithPrice(invalidPrice).Build();
        action.Should().Throw<ArgumentException>().WithMessage(Resource.InvalidPrice);
    }

    [Fact]
    public void ChangeName_ShouldUpdateCourseName()
    {
        var expectedName = _faker.Name.ToString();
        var course = CourseBuilder.New().Build();

        course.ChangeName(expectedName);

        course.Name.Should().Be(expectedName);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ChangeName_InvalidName_ShouldThrowArgumentException(string invalidName)
    {
        Action action = () => CourseBuilder.New().Build().ChangeName(invalidName);
        action.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void ChangeWorkload_ShouldUpdateCourseWorkload()
    {
        var expectedWorkload = 55;
        var course = CourseBuilder.New().Build();

        course.ChangeWorkload(expectedWorkload);

        course.Workload.Should().Be(expectedWorkload);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    [InlineData(-100)]
    public void ChangeWorkload_InvalidWorkload_ShouldThrowArgumentException(double invalidWorkload)
    {
        Action action = () => CourseBuilder.New().Build().ChangeWorkload(invalidWorkload);
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ChangePrice_ShouldUpdateCoursePrice()
    {
        var expectedPrice = 1599;
        var course = CourseBuilder.New().Build();

        course.ChangePrice(expectedPrice);

        course.Price.Should().Be(expectedPrice);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void ChangePrice_InvalidPrice_ShouldThrowArgumentException(double invalidPrice)
    {
        Action action = () => CourseBuilder.New().Build().ChangePrice(invalidPrice);
        action.Should().Throw<ArgumentException>();
    }
}