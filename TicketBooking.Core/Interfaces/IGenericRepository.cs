using System.Linq.Expressions;

namespace TicketBooking.Core.Interfaces;
public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetAll(params string[] includes);
    IQueryable<T> GetWhere(Expression<Func<T, bool>> method, params string[] includes);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> method, params string[] includes);
    Task<T> GetByIdAsync(Guid id, params string[] includes);

    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
}
