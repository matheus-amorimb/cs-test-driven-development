namespace CoursePlataform.Domain.Courses;

public record CourseEditDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Workload { get; set; }
    public string? TargetAudience { get; set; }
    public double Price { get; set; }
}