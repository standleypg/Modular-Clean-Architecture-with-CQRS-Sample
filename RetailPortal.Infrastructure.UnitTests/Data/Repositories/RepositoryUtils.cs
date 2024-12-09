using RetailPortal.Domain.Entities;
using RetailPortal.Domain.Entities.Common.ValueObjects;
using RetailPortal.Infrastructure.Data.UnitOfWork;

namespace RetailPortal.Infrastructure.UnitTests.Data.Repositories;

public class RepositoryUtils: BaseRepositoryTests
{
    private readonly UnitOfWork _uow;
    public RepositoryUtils()
    {
        _uow = new UnitOfWork(Context);
    }
    public async Task<IQueryable<Product>> CreateMockProducts(int count = 1,
        CancellationToken cancellationToken = default)
    {
        for (var i = 1; i <= count; i++)
        {
            var product = Product.Create($"Product {i}", $"Description {i}", Price.Create(i, "MYR"), i, null);
            product.AddCategory(Guid.NewGuid());
            product.AddSeller(Guid.NewGuid());
            await _uow.ProductRepository.AddAsync(product, cancellationToken);
        }

        await _uow.SaveChangesAsync(cancellationToken);

        return _uow.ProductRepository.GetAll();
    }
}