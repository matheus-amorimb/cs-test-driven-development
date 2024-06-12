using CoursePlataform.Domain.Courses;
using CoursePlataform.Domain.Utilities;

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

        if (courseSaved != null) throw new ArgumentException(Resource.NameAlreadyInUse);
        
        TargetAudience targetAudience = CheckTargetAudience(courseDto.TargetAudience);
        
        var course = new Course(courseDto.Name, courseDto.Workload, targetAudience, courseDto.Price,
            courseDto.Description);
        
        _courseRepository.Add(course);
    }
    
    private TargetAudience CheckTargetAudience(string? name)
    {
        bool tryParse = Enum.TryParse<TargetAudience>(name, out var targetAudience);

        if (!tryParse) throw new ArgumentException(Resource.TargetAudienceInvalid);

        return targetAudience;
    }

    public async void Update(CourseEditDto courseEditDto)
    {
        var course = await _courseRepository.GetById(courseEditDto.Id);

        if (course == null)
        {
            throw new ArgumentException();
        }
        
        TargetAudience targetAudience = CheckTargetAudience(courseEditDto.TargetAudience);
        
        course.ChangeName(courseEditDto.Name);
        course.ChangePrice(courseEditDto.Price);
        course.ChangeWorkload(courseEditDto.Workload);
        
        _courseRepository.Update(course);
    }
}