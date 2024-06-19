using CoursePlataform.Domain.Courses;

namespace CoursePlataform.Domain.Students;

public class StudentStorage
{
    private readonly IStudentRepository _studentRepository;

    public StudentStorage(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task Add(StudentDto studentDto)
    {
        var studentSaved = await _studentRepository.GetByCpf(studentDto.Cpf);

        if (studentSaved != null) throw new ArgumentException();
        
        var student = new Student(studentDto.Name, studentDto.Cpf, studentDto.Cpf, studentDto.TargetAudience);
        
        await _studentRepository.Add(student);
    }

    public async Task Update(StudentDto studentDto)
    {
        var studentToUpdate = await _studentRepository.GetByCpf(studentDto.Cpf);

        if (studentToUpdate == null) throw new ArgumentException();
        
        studentToUpdate?.ChangeName(studentDto.Name);
        studentToUpdate?.ChangeEmail(studentDto.Email);

        await _studentRepository.Update(studentToUpdate);
    }
}