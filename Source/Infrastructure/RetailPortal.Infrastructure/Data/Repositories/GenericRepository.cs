using ErrorOr;
using Microsoft.EntityFrameworkCore;
using RetailPortal.Domain.Interfaces.Repositories;
using RetailPortal.Infrastructure.Data.Context;

namespace RetailPortal.Infrastructure.Data.Repositories;

public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T>
    where T : class
{
    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await context.Set<T>().FindAsync([id], cancellationToken);

        if (result is null)
        {
            throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with id {id} not found.");
        }

        return result;
    }

    public IQueryable<T> GetAll()
    {
        return context.Set<T>().AsQueryable();
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