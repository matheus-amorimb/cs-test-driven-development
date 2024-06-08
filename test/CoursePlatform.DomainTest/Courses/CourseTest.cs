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
    public CourseTest(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _outputHelper.WriteLine("Constructor being executed...");
        
        _name = "Clean Architecture";
        _workload = 24;
        _targetAudience = TargetAudience.Employee;
        _price = 1299;
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
        };
        
        var course = new Course(expectedCourse.Name, expectedCourse.Workload, expectedCourse.TargetAudience, expectedCourse.Price);
        
        course.Should().BeEquivalentTo(expectedCourse);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void CourseMustNotHaveAInvalidName(string invalidName)
    {
        Action action = () => new Course(invalidName, _workload, _targetAudience, _price);
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    [InlineData(-100)]
    public void CourseMustHaveAtLeastOneHourLength(double workLoad)
    {
        Assert.Throws<ArgumentException>(() => new Course(_name, workLoad, _targetAudience, _price)).WithMessage("Course must have at least one hour length");
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void CourseMustNotHaveAPriceLowerThan1(double price)
    {
        Action action = () => new Course(_name, _workload, _targetAudience, price);
        action.Should().Throw<ArgumentException>().WithMessage("Course price must be greater than 1");
    }
}

public enum TargetAudience{
    Student,
    Undergraduate,
    Employee,
    Entrepreneur
}

public class Course
{
    public string Name { get; private set; }
    public double Workload { get; private set; }
    public TargetAudience TargetAudience { get; private set; }
    public double Price { get; private set; }
    public Course(string name, double workload, TargetAudience targetAudience, double price)
    {
        Name = CheckName(name);
        Workload = CheckWorkLoad(workload);
        TargetAudience = targetAudience;
        Price = CheckPrice(price);
    }

    private double CheckPrice(double price)
    {
        if (price < 1) throw new ArgumentException("Course price must be greater than 1");
        return price;
    }

    private double CheckWorkLoad(double workload)
    {
        if (workload < 1) throw new ArgumentException("Course must have at least one hour length");
        return workload;
    }

    private string? CheckName(string name)
    {
        if (name is null) throw new ArgumentNullException(nameof(name));
        if (name == String.Empty) throw new ArgumentException("Name can not be empty.");
        return name;
    }
}