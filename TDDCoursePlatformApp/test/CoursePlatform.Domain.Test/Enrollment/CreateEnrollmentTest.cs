using CoursePlataform.Domain.Courses;
using CoursePlataform.Domain.Enrollments;
using CoursePlataform.Domain.Students;
using CoursePlataform.DomainTest.Builders;
using CoursePlataform.DomainTest.Courses;
using FluentAssertions;
using Moq;

namespace CoursePlataform.DomainTest.Enrollment;

public class CreateEnrollmentTest
{
    private readonly EnrollmentStorage _enrollmentStorage;
    private readonly Mock<IStudentRepository> _studentRepositoryMock;
    private readonly Mock<ICourseRepository> _courseRepositoryMock;
    private readonly Mock<IEnrollmentRepository> _enrollmentRepositoryMock;
    private readonly EnrollmentDto _enrollmentDto;
    
    public CreateEnrollmentTest()
    {
        _studentRepositoryMock = new Mock<IStudentRepository>();
        _courseRepositoryMock = new Mock<ICourseRepository>();
        _enrollmentRepositoryMock = new Mock<IEnrollmentRepository>();

        _enrollmentStorage = new EnrollmentStorage(_courseRepositoryMock.Object, _studentRepositoryMock.Object,
            _enrollmentRepositoryMock.Object);
        
        var course = CourseBuilder.New().Build();
        var student = StudentBuilder.New().Build();
        _courseRepositoryMock.Setup(r => r.GetById(course.Id)).ReturnsAsync(course);
        _studentRepositoryMock.Setup(r => r.GetById(student.Id)).ReturnsAsync(student);
        _enrollmentDto = new EnrollmentDto()
            { CourseId = course.Id, StudentId = student.Id, PricePayed = (decimal)course.Price };
    }

    [Fact]
    public void Add_Enrollment_Successfully()
    {
        _enrollmentStorage.Add(_enrollmentDto);
        _enrollmentRepositoryMock.Verify(repository => 
            repository.Add(It.Is<Domain.Enrollments.Enrollment>(enrollment => enrollment.Student.Id == _enrollmentDto.StudentId && enrollment.Course.Id == _enrollmentDto.CourseId)));
    }
    
    [Fact]
    public void Add_EnrollmentWithDifferentTargetAudienceForStudentAndCourse_ThrowsAnException()
    {
        Func<Task> action = async () => await _enrollmentStorage.Add(_enrollmentDto);
        action.Should().ThrowAsync<ArgumentException>();
    }
}