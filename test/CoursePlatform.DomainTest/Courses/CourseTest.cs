namespace CoursePlataform.DomainTest.Courses;

public class CourseTest
{
    [Fact]
    public void MustCreateCourse()
    {
        //Arrange
        const string name = "Clean Architecture";
        const double workload = 24;
        const string targetAudience = "Software developers";
        const double price = 1299;
        
        //Action
        var course = new Course(name, workload, targetAudience, price);
        
        //Assert
        Assert.Equal(name, course.Name);
        Assert.Equal(workload, course.Workload);
        Assert.Equal(targetAudience, course.TargetAudience);
        Assert.Equal(price, course.Price);
    }
    
}

public class Course
{
    public string Name { get; private set; }
    public double Workload { get; private set; }
    public string TargetAudience { get; private set; }
    public double Price { get; private set; }
    public Course(string name, double workload, string targetAudience, double price)
    {
        Name = name;
        Workload = workload;
        TargetAudience = targetAudience;
        Price = price;
    }
}