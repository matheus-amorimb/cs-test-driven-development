using CoursePlataform.Domain.Students;
using CoursePlataform.DomainTest.Courses;

namespace CoursePlataform.Domain.Enrollments;

public class EnrollmentStorage
{
    private readonly ICourseRepository _courseRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;

    public EnrollmentStorage(ICourseRepository courseRepository, IStudentRepository studentRepository, IEnrollmentRepository enrollmentRepository)
    {
        _courseRepository = courseRepository;
        _studentRepository = studentRepository;
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task Add(EnrollmentDto enrollmentDto)
    {
        var course = await _courseRepository.GetById(enrollmentDto.CourseId);
        var student = await _studentRepository.GetById(enrollmentDto.StudentId);

        if (course.TargetAudience != student.TargetAudience) throw new ArgumentException();

        var enrollment = new Enrollment(student, course, enrollmentDto.PricePayed);

        await _enrollmentRepository.Add(enrollment);
    }
    
}