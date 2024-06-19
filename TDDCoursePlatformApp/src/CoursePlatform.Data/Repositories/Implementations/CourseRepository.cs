using CoursePlataform.Domain.Courses;
using CoursePlataform.DomainTest.Courses;
using CoursePlatform.Data.Context;

namespace CoursePlatform.Data.Repositories.Implementations;

public class CourseRepository : IRepository<Course>, ICourseRepository
{
    public CourseRepository(AppDbContext context) : base(context)
    { }

    public Course? GetByName(string? name)
    {
        var course = Context.Set<Course>().FirstOrDefault(c => c.Name == name);
        return course;
    }
}