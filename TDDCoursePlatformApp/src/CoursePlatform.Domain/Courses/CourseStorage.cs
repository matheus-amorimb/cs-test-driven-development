using CoursePlataform.Domain.Courses;

namespace CoursePlataform.DomainTest.Courses;

public class CourseStorage
{
    private readonly ICourseRepository _courseRepository;

    public CourseStorage(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public void Add(CourseDto courseDto)
    {
        var courseSaved = _courseRepository.GetByName(courseDto.Name);

        if (courseSaved != null) throw new ArgumentException("Course name already in use.");
        
        TargetAudience targetAudience = CheckTargetAudience(courseDto.TargetAudience);
        
        var course = new Course(courseDto.Name, courseDto.Workload, targetAudience, courseDto.Price,
            courseDto.Description);
        
        _courseRepository.Add(course);
    }

    private TargetAudience CheckTargetAudience(string? name)
    {
        bool tryParse = Enum.TryParse<TargetAudience>(name, out var targetAudience);

        if (!tryParse) throw new ArgumentException("Target audience invalid.");

        return targetAudience;
    }
}