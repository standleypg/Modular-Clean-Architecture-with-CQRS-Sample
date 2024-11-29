using RetailPortal.Core.Entities.Common.Base;

namespace RetailPortal.Core.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<Result<T>> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Result<IReadOnlyList<T>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<T>> AddAsync(T entity, CancellationToken cancellationToken);
    Task Update(T entity);
    Task Delete(T entity);
}