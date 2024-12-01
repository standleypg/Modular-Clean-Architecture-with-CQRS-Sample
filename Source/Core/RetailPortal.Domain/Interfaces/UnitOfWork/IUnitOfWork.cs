using RetailPortal.Domain.Interfaces.Repositories;

namespace RetailPortal.Domain.Interfaces.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    IProductRepository ProductRepository { get; }
    IRoleRepository RoleRepository { get; }
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}