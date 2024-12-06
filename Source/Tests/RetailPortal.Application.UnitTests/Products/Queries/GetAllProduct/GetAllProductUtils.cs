using Microsoft.EntityFrameworkCore;
using RetailPortal.Domain.Entities;
using RetailPortal.Domain.Entities.Common.ValueObjects;
using RetailPortal.Infrastructure.Data.Context;

namespace RetailPortal.Unit.Products.Queries.GetAllProduct;

public static class GetAllProductUtils
{
    public static async Task<IQueryable<Product>> CreateMockProducts(int count = 1,
        CancellationToken cancellationToken = default)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        // Populate the in-memory database
        var context = new ApplicationDbContext(options);
        for (int i = 1; i <= count; i++)
        {
            var product = Product.Create($"Product {i}", $"Description {i}", Price.Create(i, "MYR"), i, null);
            product.AddCategory(Guid.NewGuid());
            product.AddSeller(Guid.NewGuid());
            await context.Products.AddAsync(product, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);

        return context.Products;
    }
}