using System.Linq.Expressions;
using Library.Core.Repositories;
using Library.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.GenericRepository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> GetAllAsync()
    {
        return _context.Set<T>().AsQueryable().AsNoTracking();
    }

    public ValueTask<T?> GetByIdAsync(int id)
    {
        return _context.Set<T>().FindAsync(id);
    }

    public async ValueTask CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().Where(predicate).AsNoTracking();
    }
}