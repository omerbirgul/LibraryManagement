using System.Linq.Expressions;

namespace Library.Core.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<IQueryable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task CreateAsync(T entity);
    void Delete(T entity);
    void Update(T entity);
    IQueryable<T> Where(Expression<Func<T, bool>> predicate);
}