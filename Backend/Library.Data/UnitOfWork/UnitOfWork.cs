using Library.Core.UnitOfWork;
using Library.Data.Database;

namespace Library.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    public void SaveChanges() => _context.SaveChanges();
}