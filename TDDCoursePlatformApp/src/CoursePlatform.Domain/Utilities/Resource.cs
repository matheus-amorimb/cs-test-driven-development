namespace CoursePlataform.Domain.Utilities;

public static class Resource
{
    public static readonly string InvalidName = "Invalid name"; 
    public static readonly string InvalidWorkload = "Course must have at least one hour length"; 
    public static readonly string InvalidPrice = "Course price must be greater than 1";
    public static readonly string NameAlreadyInUse = "Course name already in use";
    public static readonly string TargetAudienceInvalid = "Target audience invalid";
    public static readonly string CourseNotFound = "Course not found";
    public static readonly string InvalidCpf = "Cpf must contain 11 digits";
    public static readonly string InvalidEmail = "Invalid email";
}