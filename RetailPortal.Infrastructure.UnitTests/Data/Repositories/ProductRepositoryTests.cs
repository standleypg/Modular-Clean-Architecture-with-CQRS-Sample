using Microsoft.EntityFrameworkCore;
using RetailPortal.Domain.Entities;
using RetailPortal.Infrastructure.Data.UnitOfWork;
using RetailPortal.Infrastructure.UnitTests.Data.Repositories.Common;

namespace RetailPortal.Infrastructure.UnitTests.Data.Repositories;

public class ProductRepositoryTests : BaseRepositoryTests
{
    private readonly UnitOfWork _uow;

    public ProductRepositoryTests()
    {
        _uow = new UnitOfWork(Context);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProduct()
    {
        // Arrange
        var product = (await CreateProduct())[0];

        // Act
        var result = await _uow.ProductRepository.GetByIdAsync(product.Id, CancellationToken.None);

        // Assert
        Assert.Equal(product, result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _uow.ProductRepository.GetByIdAsync(guid, CancellationToken.None));

        // Assert
        Assert.Equal($"Entity of type {nameof(Product)} with id {guid} not found.", exception.Message);
        Assert.IsType<KeyNotFoundException>(exception);
    }

    [Fact]
    public async Task GetAllProducts_ShouldReturnAllProducts()
    {
        // Arrange
        var products = await CreateProduct(10);

        // Act
        var result = await _uow.ProductRepository.GetAll().ToListAsync();

        // Assert
        Assert.Equal(products, result);
        Assert.Equal(products.Count, result.Count);
    }
    
    [Fact]
    public async Task AddAsync_ShouldAddProduct()
    {
        // Arrange
        var product = (await CreateProduct())[0];

        // Act
        var result = await _uow.ProductRepository.AddAsync(product, CancellationToken.None);

        // Assert
        Assert.Equal(product, result);
    }
    
    [Fact]
    public async Task Update_ShouldUpdateProduct()
    {
        // Arrange
        var product = (await CreateProduct())[0];
        product.Update("Updated Name", "Updated Description");
        
        // Act
        await _uow.ProductRepository.Update(product);
        await _uow.SaveChangesAsync(CancellationToken.None);
        var result = await _uow.ProductRepository.GetByIdAsync(product.Id, CancellationToken.None);

        // Assert
        Assert.Equal(product, result);
    }
    
    [Fact]
    public async Task Delete_ShouldDeleteProduct()
    {
        // Arrange
        var product = (await CreateProduct())[0];
        
        // Act
        await _uow.ProductRepository.Delete(product);
        await _uow.SaveChangesAsync(CancellationToken.None);
        var result = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _uow.ProductRepository.GetByIdAsync(product.Id, CancellationToken.None));

        // Assert
        Assert.IsType<KeyNotFoundException>(result);
    }

    #region private methods

    private async Task<List<Product>> CreateProduct(int count = 1)
    {
        var products = new List<Product>();
        await RepositoryUtils.CreateProduct(async (product, token) =>
        {
            products.Add(product);
            await _uow.ProductRepository.AddAsync(product, token);
            await _uow.SaveChangesAsync(token);
        }, count);

        return products;
    }

    #endregion
}