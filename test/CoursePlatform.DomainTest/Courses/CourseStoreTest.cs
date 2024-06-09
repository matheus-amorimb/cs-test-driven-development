using Bogus.DataSets;
using CoursePlataform.Domain.Courses;

namespace CoursePlataform.DomainTest.Courses;

public class CourseStoreTest
{
    [Fact]
    public void MustAddCourse()
    {
        var courseDto = new CourseDto
        {
            Name = "Clean Architecture",
            Description = "How to create a Web Api using Clean Architecture",
            Workload = 24,
            TargetAudience = TargetAudience.Employee,
            Price = 1299,
        };

        var courseStorage = new CourseStorage();

        courseStorage.Add(courseDto);
    }
}

public class CourseStorage
{
    public void Add(CourseDto courseDto)
    {
        throw new System.NotImplementedException();
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
    public TargetAudience TargetAudience { get; set; }
    public double Price { get; set; }
}