using System.ComponentModel.DataAnnotations;

namespace CoursePlataform.Domain.Base;

public abstract class Entity
{
    [Key]
    public Guid Id { get; protected set; }
}