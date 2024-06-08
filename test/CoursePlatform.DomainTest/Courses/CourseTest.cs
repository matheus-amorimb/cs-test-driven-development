using CoursePlataform.DomainTest.Utilities;
using FluentAssertions;

namespace CoursePlataform.DomainTest.Courses;

public class CourseTest
{
    [Fact]
    public void MustCreateCourse()
    {
        var expectedCourse = new
        {
            Name = "Clean Architecture",
            Workload = 24,
            TargetAudience = TargetAudience.Employee,
            Price = 1299,
            
        };
        
        var course = new Course(expectedCourse.Name, expectedCourse.Workload, expectedCourse.TargetAudience, expectedCourse.Price);
        
        course.Should().BeEquivalentTo(expectedCourse);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void CourseMustNotHaveAInvalidName(string invalidName)
    {
        var expectedCourse = new
        {
            Name = "Clean Architecture",
            Workload = 24,
            TargetAudience = TargetAudience.Employee,
            Price = 1299,
        };
        
        Action action = () => new Course(invalidName, expectedCourse.Workload, expectedCourse.TargetAudience,
            expectedCourse.Price);
        
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    [InlineData(-100)]
    public void CourseMustHaveAtLeastOneHourLength(double workLoad)
    {
        var expectedCourse = new
        {
            Name = "Clean Architecture",
            Workload = 60,
            TargetAudience = TargetAudience.Employee,
            Price = 1299,
        };

        Assert.Throws<ArgumentException>(() => new Course(expectedCourse.Name, workLoad, expectedCourse.TargetAudience, expectedCourse.Price)).WithMessage("Course must have at least one hour length");
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void CourseMustNotHaveAPriceLowerThan1(double price)
    {
        var expectedCourse = new
        {
            Name = "Clean Architecture",
            Workload = 24,
            TargetAudience = TargetAudience.Employee,
            Price = 1299,
        };
        
        Action action = () => new Course(expectedCourse.Name, expectedCourse.Workload, expectedCourse.TargetAudience,
            price);

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