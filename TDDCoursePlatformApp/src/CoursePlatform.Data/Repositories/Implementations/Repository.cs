using System.Linq.Expressions;
using CoursePlataform.Domain.Base;
using CoursePlatform.Data.Context;
using CoursePlatform.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform.Data.Repositories.Implementations;

public class Repository<T> : IRepository<T> where T : Entity
{

    protected readonly AppDbContext Context;

    public Repository(AppDbContext context)
    {
        Context = context;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await Context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetById(Guid id)
    {
        return await Context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<T> Add(T entity)
    {
        Context.Set<T>().Add(entity);
        return entity;
    }

    public T Update(T entity)
    {
        Context.Set<T>().Update(entity);
        return entity;
    }

    public T Delete(T entity)
    {
        Context.Set<T>().Remove(entity);
        return entity;
    }
}