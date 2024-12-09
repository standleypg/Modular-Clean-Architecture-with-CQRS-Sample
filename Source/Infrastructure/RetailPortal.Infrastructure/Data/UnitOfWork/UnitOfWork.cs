using Microsoft.EntityFrameworkCore.Storage;
using RetailPortal.Domain.Interfaces.Repositories;
using RetailPortal.Domain.Interfaces.UnitOfWork;
using RetailPortal.Infrastructure.Data.Context;
using RetailPortal.Infrastructure.Data.Repositories;

namespace RetailPortal.Infrastructure.Data.UnitOfWork;

public sealed class UnitOfWork(ApplicationDbContext context)
    : IUnitOfWork, IAsyncDisposable
{
    private IDbContextTransaction? _currentTransaction;
    private IUserRepository _userRepository;
    public IUserRepository UserRepository => this._userRepository ??= new UserRepository(context);
    private IProductRepository _productRepository;
    public IProductRepository ProductRepository => this._productRepository ??= new ProductRepository(context);
    private IRoleRepository _roleRepository;
    public IRoleRepository RoleRepository => this._roleRepository ??= new RoleRepository(context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (this._currentTransaction != null) return;
        this._currentTransaction = await context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (this._currentTransaction == null) return;

            await context.SaveChangesAsync(cancellationToken);
            await this._currentTransaction.CommitAsync(cancellationToken);
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

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (this._currentTransaction != null)
            {
                await this._currentTransaction.RollbackAsync(cancellationToken);
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
        this._currentTransaction?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    ~UnitOfWork()
    {
        this.Dispose();
    }
}