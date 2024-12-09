namespace RetailPortal.Domain.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    IQueryable<T> GetAll();
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    Task Update(T entity);
    Task Delete(T entity);
}