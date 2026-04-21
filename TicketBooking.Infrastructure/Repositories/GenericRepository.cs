using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketBooking.Core.Interfaces;
using TicketBooking.Infrastructure.Data;

namespace TicketBooking.Infrastructure.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));
        return query;
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, params Expression<Func<T, object>>[] includes)
    {
        var query = _dbSet.Where(method);
        if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));
        return query;
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, params Expression<Func<T, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.FirstOrDefaultAsync(method);
    }

    public async Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
    }

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
    public void Update(T entity) => _dbSet.Update(entity);
    public void Remove(T entity) => _dbSet.Remove(entity);
}
