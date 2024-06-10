using CoursePlatform.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CoursePlatform.Web.Middleware;

public class SaveChangesMiddleware
{
    private readonly RequestDelegate _next;

    public SaveChangesMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUnitOfWork unitOfWork)
    {
        await _next(context);

        await unitOfWork.Commit();
    }
}