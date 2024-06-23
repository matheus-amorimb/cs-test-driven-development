using CoursePlataform.Domain.Enrollments;
using CoursePlatform.Data.Context;

namespace CoursePlatform.Data.Repositories.Implementations;

public class EnrollmentRepository : Repository<Enrollment>
{
    public EnrollmentRepository(AppDbContext context) : base(context)
    {
    }
}