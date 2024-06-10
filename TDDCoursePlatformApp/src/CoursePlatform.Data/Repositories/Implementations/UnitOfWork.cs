using CoursePlatform.Data.Context;
using CoursePlatform.Data.Repositories.Interfaces;

namespace CoursePlatform.Data.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}