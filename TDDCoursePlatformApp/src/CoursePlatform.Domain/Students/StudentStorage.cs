using CoursePlataform.Domain.Courses;

namespace CoursePlataform.Domain.Students;

public class StudentStorage
{
    private readonly IStudentRepository _studentRepository;

    public StudentStorage(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public void Add(StudentDto studentDto)
    {
        var student = new Student(studentDto.Name, studentDto.Cpf, studentDto.Cpf, studentDto.TargetAudience);
        
        _studentRepository.Add(student);
    }
    
}