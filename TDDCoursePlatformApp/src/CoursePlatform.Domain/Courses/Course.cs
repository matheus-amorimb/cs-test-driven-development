using System.ComponentModel.DataAnnotations.Schema;
using CoursePlataform.Domain.Base;
using CoursePlataform.Domain.Utilities;

namespace CoursePlataform.Domain.Courses;

[Table("course")]
public class Course : Entity
{
    public string? Name { get; private set; }
    public double Workload { get; private set; }
    public TargetAudience TargetAudience { get; private set; }
    public double Price { get; private set; }
    public string? Description { get; private set; }
    public Course(string? name, double workload, TargetAudience targetAudience, double price, string? description)
    {
        Name = CheckName(name);
        Workload = CheckWorkload(workload);
        TargetAudience = targetAudience;
        Price = CheckPrice(price);
        Description = description;
    }
    
    public void ChangeName(string name)
    {
        this.Name = CheckName(name);
    }

    public void ChangeWorkload(double workload)
    {
        this.Workload = CheckWorkload(workload);
    }

    public void ChangePrice(double price)
    {
        this.Price = CheckPrice(price);
    }
    
    private double CheckPrice(double price)
    {
        if (price < 1) throw new ArgumentException(Resource.InvalidPrice);
        return price;
    }

    private double CheckWorkload(double workload)
    {
        if (workload < 1) throw new ArgumentException(Resource.InvalidWorkload);
        return workload;
    }

    private string? CheckName(string? name)
    {
        if (name is null) throw new ArgumentNullException(nameof(name));
        if (name == String.Empty) throw new ArgumentException(Resource.InvalidName);
        return name;
    }
}