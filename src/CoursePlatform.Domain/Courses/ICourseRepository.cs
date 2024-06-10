using CoursePlataform.Domain.Courses;
using CoursePlatform.Data.Repositories.Interfaces;

namespace CoursePlataform.DomainTest.Courses;

public interface ICourseRepository : IRepository<Course>
{
    Course? GetByName(string? name);
}