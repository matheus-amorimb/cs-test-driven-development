namespace CoursePlataform.Domain.Enrollments;

public class EnrollmentDto
{
    public Guid StudentId { get; set;}
    public Guid CourseId { get; set;}
    public decimal PricePayed { get; set;}
} 