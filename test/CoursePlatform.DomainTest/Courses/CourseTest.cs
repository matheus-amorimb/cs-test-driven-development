using FluentAssertions;

namespace CoursePlataform.DomainTest.Courses;

public class CourseTest
{
    [Fact]
    public void MustCreateCourse()
    {
        //Arrange
        var expectedCourse = new
        {
            Name = "Clean Architecture",
            Workload = 24,
            TargetAudience = TargetAudience.Employee,
            Price = 1299,
        };
        
        //Action
        var course = new Course(expectedCourse.Name, expectedCourse.Workload, expectedCourse.TargetAudience, expectedCourse.Price);
        
        //Assert
        course.Should().BeEquivalentTo(expectedCourse);
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
        Name = name;
        Workload = workload;
        TargetAudience = targetAudience;
        Price = price;
    }
}