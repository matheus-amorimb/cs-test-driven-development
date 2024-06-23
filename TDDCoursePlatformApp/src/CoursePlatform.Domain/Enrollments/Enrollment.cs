using CoursePlataform.Domain.Base;
using CoursePlataform.Domain.Courses;
using CoursePlataform.Domain.Students;

namespace CoursePlataform.Domain.Enrollments;

public class Enrollment : Entity
{
    public Student Student { get; private set; } 
    public Course Course { get; private set; } 
    public decimal PricePayed { get; private set; }
    public bool HasDiscount { get; private set; } = false;
    public double Grade { get; private set; }
    public bool Completed { get; private set; } = false;

    public Enrollment(Student student, Course course, decimal pricePayed)
    {
        SetStudent(student);
        SetCourse(course);
        SetPricePayed(pricePayed);
        CheckStudentAndCourseHasSameTargetAudience();
    }

    private void CheckStudentAndCourseHasSameTargetAudience()
    {
        if (Student.TargetAudience != Course.TargetAudience) throw new ArgumentException();
    }

    private void SetPricePayed(decimal pricePayed)
    {
        if (pricePayed < 0) throw new ArgumentException();
        if (pricePayed > (decimal)Course.Price) throw new ArgumentException();
        if (pricePayed < (decimal)Course.Price) HasDiscount = true;
        PricePayed = pricePayed;
    }

    private void SetStudent(Student student)
    {
        Student = student ?? throw new ArgumentException();
    }    
    private void SetCourse(Course course)
    {
        Course = course ?? throw new ArgumentException();
    }

    public void SetGrade(double grade)
    {
        Grade =  grade is < 0 or > 10 ? throw new ArgumentException() : grade;
        Completed = true;
    }
}