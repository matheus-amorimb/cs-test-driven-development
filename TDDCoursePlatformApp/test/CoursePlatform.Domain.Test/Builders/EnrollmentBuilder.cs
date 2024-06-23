using CoursePlataform.Domain.Courses;
using CoursePlataform.Domain.Students;

namespace CoursePlataform.DomainTest.Builders;

public class EnrollmentBuilder
{
    private Student _student = StudentBuilder.New().WithTargetAudience(TargetAudience.Employee).Build();
    private Course _course = CourseBuilder.New().WithTargetAudience(TargetAudience.Employee).Build();
    private decimal _pricePayed = 1000m;

    public static EnrollmentBuilder New()
    {
        return new EnrollmentBuilder();
    }

    public EnrollmentBuilder WithStudent(Student student)
    {
        _student = student;
        return this;
    }
    
    public EnrollmentBuilder WithCourse(Course course)
    {
        _course = course;
        return this;
    }
    
    public EnrollmentBuilder WithPricePayed(decimal price)
    {
        _pricePayed = price;
        return this;
    }
    
    public Domain.Enrollments.Enrollment Build()
    {
        return new Domain.Enrollments.Enrollment(_student, _course, _pricePayed);
    }
    
}