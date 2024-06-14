using System.ComponentModel.DataAnnotations;
using CoursePlataform.Domain.Courses;

namespace CoursePlataform.Domain.Students;

public record StudentDto(
    string Name,
    string Cpf,
    string Email,
    TargetAudience TargetAudience
);