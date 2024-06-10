using System.Linq.Expressions;
using CoursePlataform.Domain.Base;

namespace CoursePlatform.Data.Repositories.Interfaces;

public interface IRepository<T> where T: Entity
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(Expression<Func<T, bool>> expression);
    Task<T> Add(T entity);
    T Update(T entity);
    T Delete(T entity);
}