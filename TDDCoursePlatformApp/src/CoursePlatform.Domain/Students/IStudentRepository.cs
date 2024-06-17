using CoursePlatform.Data.Repositories.Interfaces;

namespace CoursePlataform.Domain.Students;

public interface IStudentRepository : IRepository<Student>
{
    Task<Student?> GetByCpf(string cpf);
}