using RetailPortal.Domain.Entities;
using RetailPortal.Domain.Entities.Common.ValueObjects;
using RetailPortal.Infrastructure.Data.UnitOfWork;

namespace RetailPortal.Infrastructure.UnitTests.Data.Repositories.Common;

public class RepositoryUtils : BaseRepositoryTests
{
    private readonly UnitOfWork _uow;

    public RepositoryUtils()
    {
        _uow = new UnitOfWork(Context);
    }

    public async Task<IQueryable<Product>> CreateQueryableMockProducts(int count = 1,
        CancellationToken cancellationToken = default)
    {
        await CreateProduct(async (product, token) =>
            {
                await _uow.ProductRepository.AddAsync(product, token);
                await _uow.SaveChangesAsync(cancellationToken);
            }, count,
            cancellationToken);

        return _uow.ProductRepository.GetAll();
    }

    public static async Task CreateProduct(Func<Product, CancellationToken, Task>? execute, int count = 1,
        CancellationToken cancellationToken = default)
    {
        for (var i = 1; i <= count; i++)
        {
            var product = Product.Create($"Product {i}", $"Description {i}", Price.Create(i, "MYR"), i, null);
            product.AddCategory(Guid.NewGuid());
            product.AddSeller(Guid.NewGuid());
            if (execute != null)
            {
                await execute(product, cancellationToken);
            }
        }
    }
}