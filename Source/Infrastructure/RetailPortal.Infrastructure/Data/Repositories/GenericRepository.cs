using Microsoft.EntityFrameworkCore;
using RetailPortal.Core.Entities.Common.Base;
using RetailPortal.Core.Interfaces.Repositories;
using RetailPortal.Infrastructure.Data.Context;

namespace RetailPortal.Infrastructure.Data.Repositories;
public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T>
    where T : class
{
    public async Task<Result<T>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await context.Set<T>().FindAsync([id], cancellationToken);

        return result == null ? Result<T>.Failure("Entity not found") : Result<T>.Success(result);
    }

    public async Task<Result<IReadOnlyList<T>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await context.Set<T>().ToListAsync(cancellationToken);
        return Result<IReadOnlyList<T>>.Success(result);
    }

    public async Task<Result<T>> AddAsync(T entity, CancellationToken cancellationToken)
    {
        await context.Set<T>().AddAsync(entity, cancellationToken);
        return Result<T>.Success(entity);
    }

    public async Task Update(T entity)
    {
        await Task.CompletedTask;
        context.Entry(entity).State = EntityState.Modified;
    }

    public async Task Delete(T entity)
    {
        await Task.CompletedTask;
        context.Set<T>().Remove(entity);
    }
}