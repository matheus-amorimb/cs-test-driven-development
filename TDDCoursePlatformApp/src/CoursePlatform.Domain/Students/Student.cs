using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using CoursePlataform.Domain.Base;
using CoursePlataform.Domain.Courses;
using CoursePlataform.Domain.Utilities;

namespace CoursePlataform.Domain.Students;

[Table("student")]
public class Student : Entity
{
    public string? Name { get; private set; }
    public string? Cpf { get; private set; }
    public string? Email { get; private set; }
    public TargetAudience TargetAudience { get; private set; }

    public Student(string name, string cpf, string email, TargetAudience targetAudience)
    {
        Name = CheckName(name);
        Cpf = CheckCpf(cpf);
        Email = CheckEmail(email);
        TargetAudience = targetAudience;
    }

    private string? CheckEmail(string email)
    {
        if (String.IsNullOrEmpty(email)) throw new ArgumentException(Resource.InvalidEmail);

        return email;
    }

    private string? CheckCpf(string cpf)
    {
        if (String.IsNullOrEmpty(cpf)) throw new ArgumentException(Resource.InvalidCpf);

        return cpf;
    }

    private string? CheckName(string name)
    {
        if (String.IsNullOrEmpty(name)) throw new ArgumentException(Resource.InvalidName);

        return name;
    }

    public void ChangeName(string newName)
    {
        Name = CheckName(newName);
    }
    public void ChangeEmail(string newEmail)
    {
        Email = CheckEmail(newEmail);
    }
}