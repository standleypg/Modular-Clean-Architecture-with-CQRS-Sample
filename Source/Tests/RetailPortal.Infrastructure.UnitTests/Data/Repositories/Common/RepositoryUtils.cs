using RetailPortal.Domain.Entities;
using RetailPortal.Domain.Entities.Common;
using RetailPortal.Domain.Entities.Common.ValueObjects;
using RetailPortal.Infrastructure.Data.UnitOfWork;

namespace RetailPortal.Infrastructure.UnitTests.Data.Repositories.Common;

public class RepositoryUtils : BaseRepositoryTests
{
    private readonly UnitOfWork _uow;

    public RepositoryUtils()
    {
        this._uow = new UnitOfWork(Context);
    }

    public async Task<IQueryable<Product>> CreateQueryableMockProducts(int count = 1,
        CancellationToken cancellationToken = default)
    {
        await CreateEntity(CreateProduct, async (product, token) =>
            {
                await this._uow.ProductRepository.AddAsync(product, token);
                await this._uow.SaveChangesAsync(cancellationToken);
            }, count,
            cancellationToken);

        return this._uow.ProductRepository.GetAll();
    }

    public static async Task CreateEntity<T>(Func<int, T> createEntity, Func<T, CancellationToken, Task>? execute, int count = 1,
        CancellationToken cancellationToken = default)
    {
        for (var i = 1; i <= count; i++)
        {
            var entity = createEntity(i);
            if (execute != null)
            {
                await execute(entity, cancellationToken);
            }
        }
    }

    public static Product CreateProduct(int i)
    {
        var product = Product.Create($"Product {i}", $"Description {i}", Price.Create(i, "MYR"), i, null);
        product.AddCategory(Guid.NewGuid());
        product.AddSeller(Guid.NewGuid());
        return product;
    }

    public static Category CreateCategory(int i)
    {
        var category = Category.Create($"Category {i}");
        return category;
    }

    public static Role CreateRole(int i)
    {
        var role = Role.Create($"Role {i}", $"Description {i}");
        return role;
    }

    public static User CreateUser(int i)
    {
        var password = Password.Create([1, 2, 3, 4, 5, 6, 7, 8, 9, 0], [1, 2, 3, 4, 5, 6, 7, 8, 9, 0]);
        var user = User.Create($"Firstname {i}", $"Lastname {i}", $"{i}@email.com", password: password);
        return user;
    }
}