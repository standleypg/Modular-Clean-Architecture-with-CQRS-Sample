using ErrorOr;
using Microsoft.EntityFrameworkCore;
using RetailPortal.Core.Entities.Common.Base;
using RetailPortal.Core.Interfaces.Repositories;
using RetailPortal.Infrastructure.Data.Context;

namespace RetailPortal.Infrastructure.Data.Repositories;

public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T>
    where T : class
{
    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await context.Set<T>().FindAsync([id], cancellationToken);

        if (result is null)
        {
            throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with id {id} not found.");
        }

        return result;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        await context.Set<T>().AddAsync(entity, cancellationToken);
        return entity;
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