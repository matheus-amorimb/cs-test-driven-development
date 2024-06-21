using CoursePlataform.Domain.Courses;
using CoursePlataform.Domain.Students;
using CoursePlataform.DomainTest.Builders;
using FluentAssertions;

namespace CoursePlataform.DomainTest.Enrollment;

public class EnrollmentTest
{
    [Fact]
    public void Add_EnrollmentSuccessfully()
    {
        var expectedEnrollment = new
        {
            Student = StudentBuilder.New().Build(),
            Course = CourseBuilder.New().Build(),
            EnrollmentPrice = 1000
        };
        
        var enrollment = new Enrollment(expectedEnrollment.Student, expectedEnrollment.Course, expectedEnrollment.EnrollmentPrice);

        expectedEnrollment.Should().BeEquivalentTo(enrollment);
    }
}

public class Enrollment
{
    public Student Student { get; set; } 
    public Course Course { get; set; } 
    
    public decimal EnrollmentPrice { get; set; }
    
    public Enrollment(Student student, Course course, decimal enrollmentPrice)
    {
        Student = student;
        Course = course;
        EnrollmentPrice = enrollmentPrice;

    }
}