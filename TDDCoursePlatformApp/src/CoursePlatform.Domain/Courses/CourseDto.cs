namespace CoursePlataform.DomainTest.Courses;

public record CourseDto
{    
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Workload { get; set; }
    public string? TargetAudience { get; set; }
    public double Price { get; set; }
}