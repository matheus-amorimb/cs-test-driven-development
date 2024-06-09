using CoursePlataform.Domain.Courses;

namespace CoursePlataform.DomainTest.Courses;

public interface ICourseRepository
{
    void Add(Course course);
    Course GetByName(string? name);
}