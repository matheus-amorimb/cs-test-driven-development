using System.Linq.Expressions;
using CoursePlataform.Domain.Base;

namespace CoursePlatform.Data.Repositories.Interfaces;

public interface IRepository<T> where T: Entity
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(Guid id);
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    T Delete(T entity);
}