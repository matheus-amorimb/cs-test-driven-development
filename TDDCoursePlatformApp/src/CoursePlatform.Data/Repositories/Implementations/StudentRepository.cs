using CoursePlataform.Domain.Students;
using CoursePlatform.Data.Context;
using CoursePlatform.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform.Data.Repositories.Implementations;

public class StudentRepository : IRepository<Student>, IStudentRepository
{
    public StudentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Student?> GetByCpf(string cpf)
    {
        var student = await Context.Set<Student>().FirstOrDefaultAsync(student => student.Cpf == cpf);
        return student;
    }
}