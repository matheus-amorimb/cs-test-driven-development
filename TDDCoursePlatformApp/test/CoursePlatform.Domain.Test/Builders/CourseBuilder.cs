using CoursePlataform.Domain.Courses;
using CoursePlataform.DomainTest.Courses;

namespace CoursePlataform.DomainTest.Builders;

public class CourseBuilder
{
    private string? _name = "Clean Architecture";
    private double _workload = 24;
    private TargetAudience _targetAudience = TargetAudience.Employee;
    private double  _price = 1299;
    private string?  _description = "How to create a Web Api using Clean Architecture";
    private Guid _id = Guid.NewGuid();

    public static CourseBuilder New()
    {
        return new CourseBuilder();
    }

    public Course Build()
    {
        return new Course(_name, _workload, _targetAudience, _price, _description);
    }
    
    public CourseBuilder WithName(string? name)
    {
        _name = name;
        return this;
    }    
    
    public CourseBuilder WithDescription(string? description)
    {
        _description = description;
        return this;
    }    
    
    public CourseBuilder WithWorkload(double workload)
    {
        _workload = workload;
        return this;
    }    
    
    public CourseBuilder WithTargetAudience(TargetAudience targetAudience)
    {
        _targetAudience = targetAudience;
        return this;
    }

    public CourseBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }
    
    public CourseBuilder WithPrice(double price)
    {
        _price = price;
        return this;
    } 
    
}