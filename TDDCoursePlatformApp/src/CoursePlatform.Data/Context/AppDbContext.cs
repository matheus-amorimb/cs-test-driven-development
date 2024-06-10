using CoursePlataform.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> contextOptions) : base(contextOptions)
    { }
    
    public DbSet<Course> Courses { get; set; }
}