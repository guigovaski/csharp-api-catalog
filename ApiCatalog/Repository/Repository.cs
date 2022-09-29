using ApiCatalog.Data;
using ApiCatalog.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiCatalog.Repository;

public abstract class Repository<T> : IRepository<T> where T: class
{
    protected ApiCatalogContext _context;
    
    public Repository(ApiCatalogContext context)
    {
        _context = context;
    }

    public IQueryable<T> Get()
    {
        return _context.Set<T>().AsNoTracking();
    }

    public async Task<T> GetById(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(predicate);
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
}
