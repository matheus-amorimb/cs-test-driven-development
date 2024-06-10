namespace CoursePlataform.DomainTest.Courses;

public record CourseDto
{    
    public string? Name { get; init; }
    public string? Description { get; init; }
    public double Workload { get; init; }
    public string? TargetAudience { get; init; }
    public double Price { get; init; }
}