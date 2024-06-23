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
            PricePayed = 1000
        };
        
        var enrollment = new Enrollment(expectedEnrollment.Student, expectedEnrollment.Course, expectedEnrollment.PricePayed);

        expectedEnrollment.Should().BeEquivalentTo(enrollment);
    }

    [Fact]
    public void Create_EnrollmentInvalidStudent_ThrowsAnException()
    {
        Student? invalidStudent = null;
        Action action = () => EnrollmentBuilder.New().WithStudent(invalidStudent!).Build();
        action.Should().Throw<ArgumentException>();
    }    
    
    [Fact]
    public void Create_EnrollmentInvalidCourse_ThrowsAnException()
    {
        Course? invalidCourse = null;
        Action action = () => EnrollmentBuilder.New().WithCourse(invalidCourse!).Build();
        action.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void Create_EnrollmentWithPricePayedLowerThanZero_ThrowsAnException()
    {
        decimal invalidPrice = -1m;
        Action action = () => EnrollmentBuilder.New().WithPricePayed(invalidPrice!).Build();
        action.Should().Throw<ArgumentException>();
    }    
    
    [Fact]
    public void Create_EnrollmentWithPricePayedGreaterThanCoursePrice_ThrowsAnException()
    {
        double coursePrice = 1299;
        Course course = CourseBuilder.New().WithPrice(coursePrice).Build();
        decimal pricePayed = (decimal)(1.5 * coursePrice);
        Action action = () => EnrollmentBuilder.New().WithCourse(course).WithPricePayed(pricePayed).Build();
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_EnrollmentWithPriceLowerThanCoursePrice_MustSetPropHasDiscountToTrue()
    {
        double coursePrice = 1299;
        Course course = CourseBuilder.New().WithPrice(coursePrice).Build();
        decimal pricePayed = (decimal)(0.9 * coursePrice);
        var enrollment = EnrollmentBuilder.New().WithCourse(course).WithPricePayed(pricePayed).Build();
        enrollment.HasDiscount.Should().Be(true);
    }
    
}

public class Enrollment
{
    public Student Student { get; private set; } 
    public Course Course { get; private set; } 
    public decimal PricePayed { get; private set; }
    public bool HasDiscount { get; private set; } = false;
    
    public Enrollment(Student student, Course course, decimal pricePayed)
    {
        SetStudent(student);
        SetCourse(course);
        SetPricePayed(pricePayed);
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
    
}