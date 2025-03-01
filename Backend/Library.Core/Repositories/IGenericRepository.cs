using System.Linq.Expressions;

namespace Library.Core.Repositories;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetAllAsync();
    ValueTask<T?> GetByIdAsync(int id);
    ValueTask CreateAsync(T entity);
    void Delete(T entity);
    void Update(T entity);
    IQueryable<T> Where(Expression<Func<T, bool>> predicate);
}