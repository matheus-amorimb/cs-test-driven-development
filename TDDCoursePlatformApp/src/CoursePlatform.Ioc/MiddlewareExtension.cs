using CoursePlatform.Web.Middleware;

namespace CoursePlatform.Ioc;
using Microsoft.AspNetCore.Builder;

public static class MiddlewareExtension
{
    public static IApplicationBuilder UseSaveChangesMiddleware(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<SaveChangesMiddleware>();

        return applicationBuilder;
    }
}