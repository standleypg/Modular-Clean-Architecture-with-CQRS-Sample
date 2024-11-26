using Microsoft.EntityFrameworkCore.Storage;
using RetailPortal.Core.Interfaces.Repositories;
using RetailPortal.Core.Interfaces.UnitOfWork;
using RetailPortal.Infrastructure.Data.Context;
using RetailPortal.Infrastructure.Data.Repositories;

namespace RetailPortal.Infrastructure.Data.UnitOfWork;

public class UnitOfWork(ApplicationDbContext context)
    : IUnitOfWork, IAsyncDisposable
{
    private IDbContextTransaction _transaction;
    private IUserRepository _userRepository;

    public IUserRepository UserRepository => this._userRepository ??= new UserRepository(context);

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        this._transaction = await context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        try
        {
            await this.SaveChangesAsync();
            await this._transaction.CommitAsync();
        }
        catch
        {
            await this.RollbackAsync();
            throw;
        }
    }

    public async Task RollbackAsync()
    {
        await this._transaction.RollbackAsync();
        await this.DisposeAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
        await this._transaction.DisposeAsync();
    }

    public void Dispose()
    {
        context.Dispose();
        this._transaction.Dispose();
    }
}