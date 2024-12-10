using RetailPortal.Domain.Interfaces.Repositories;

namespace RetailPortal.Domain.Interfaces.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    IProductRepository ProductRepository { get; }
    IRoleRepository RoleRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
}