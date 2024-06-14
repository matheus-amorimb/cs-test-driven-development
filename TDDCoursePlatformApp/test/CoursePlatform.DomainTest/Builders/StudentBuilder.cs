using CoursePlataform.Domain.Courses;
using CoursePlataform.Domain.Students;
using CoursePlataform.DomainTest.Courses;

namespace CoursePlataform.DomainTest.Builders;

public class StudentBuilder
{
    private StudentBuilder _studentBuilder;
    private Student _student;
    private string _name = "Peter";
    private string _cpf = "14863";
    private string _email = "email@email.cpm";
    private TargetAudience _targetAudience = TargetAudience.Student;
    
    public static StudentBuilder New()
    {
        return new StudentBuilder();
    }

    public Student Build()
    {
        return new Student(_name, _cpf, _email, _targetAudience);;
    }

    public StudentBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public StudentBuilder WithCpf(string cpf)
    {
        _cpf = cpf;
        return this;
    }
    public StudentBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }
}