using CoursePlataform.Domain.Students;
using CoursePlataform.DomainTest.Courses;
using CoursePlatform.Data.Context;
using CoursePlatform.Data.Repositories.Implementations;
using CoursePlatform.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoursePlatform.Ioc;

public static class ServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(builder =>
        {
            var postgresConnection = configuration.GetConnectionString("DefaultConnection"); 
            builder.UseNpgsql(postgresConnection);
        });
        services.AddScoped(typeof(Data.Repositories.Interfaces.IRepository<>), typeof(Data.Repositories.Implementations.Repository<>));
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<CourseStorage>();
        services.AddScoped<StudentStorage>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        
        return services;
    }  
}