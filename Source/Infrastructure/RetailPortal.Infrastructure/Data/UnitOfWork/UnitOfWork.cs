using Microsoft.EntityFrameworkCore.Storage;
using RetailPortal.Core.Interfaces.Repositories;
using RetailPortal.Core.Interfaces.UnitOfWork;
using RetailPortal.Infrastructure.Data.Context;
using RetailPortal.Infrastructure.Data.Repositories;

namespace RetailPortal.Infrastructure.Data.UnitOfWork;

public class UnitOfWork(ApplicationDbContext context)
    : IUnitOfWork, IAsyncDisposable
{
    private IDbContextTransaction? _currentTransaction;
    private IUserRepository _userRepository;
    public IUserRepository UserRepository => this._userRepository ??= new UserRepository(context);
    private IProductRepository _productRepository;
    public IProductRepository ProductRepository => this._productRepository ??= new ProductRepository(context);
    private IRoleRepository _roleRepository;
    public IRoleRepository RoleRepository => this._roleRepository ??= new RoleRepository(context);

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        if (this._currentTransaction != null) return;
        this._currentTransaction = await context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        try
        {
            if (this._currentTransaction == null) return;

            await context.SaveChangesAsync();
            await this._currentTransaction.CommitAsync();
        }
        catch
        {
            await this.RollbackAsync();
            throw;
        }
        finally
        {
            if (this._currentTransaction != null)
            {
                await this._currentTransaction.DisposeAsync();
                this._currentTransaction = null;
            }
        }
    }

    public async Task RollbackAsync()
    {
        try
        {
            if (this._currentTransaction != null)
            {
                await this._currentTransaction.RollbackAsync();
            }
        }
        finally
        {
            if (this._currentTransaction != null)
            {
                await this._currentTransaction.DisposeAsync();
                this._currentTransaction = null;
            }
        }
    }

    public void Dispose()
    {
        context.Dispose();
        if (this._currentTransaction != null)
        {
            this._currentTransaction.Dispose();
        }
    }

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
    }
}