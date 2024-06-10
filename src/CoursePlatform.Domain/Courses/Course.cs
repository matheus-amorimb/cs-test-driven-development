using System.ComponentModel.DataAnnotations.Schema;
using CoursePlataform.Domain.Base;

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
        Workload = CheckWorkLoad(workload);
        TargetAudience = targetAudience;
        Price = CheckPrice(price);
        Description = description;
    }

    private double CheckPrice(double price)
    {
        if (price < 1) throw new ArgumentException("Course price must be greater than 1");
        return price;
    }

    private double CheckWorkLoad(double workload)
    {
        if (workload < 1) throw new ArgumentException("Course must have at least one hour length");
        return workload;
    }

    private string? CheckName(string? name)
    {
        if (name is null) throw new ArgumentNullException(nameof(name));
        if (name == String.Empty) throw new ArgumentException("Name can not be empty.");
        return name;
    }
}