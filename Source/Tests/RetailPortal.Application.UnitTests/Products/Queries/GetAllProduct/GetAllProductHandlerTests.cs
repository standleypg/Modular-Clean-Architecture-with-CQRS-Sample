using Moq;
using RetailPortal.Application.Products.Queries.GetAllProduct;
using RetailPortal.Domain.Entities;
using RetailPortal.Domain.Interfaces.Infrastructure.Data.Repositories;
using RetailPortal.Domain.Interfaces.Infrastructure.Data.UnitOfWork;
using RetailPortal.Infrastructure.UnitTests.Data.Repositories.Common;

namespace RetailPortal.Unit.Products.Queries.GetAllProduct;

public sealed class GetAllProductHandlerTests : IDisposable
{
    private readonly Mock<IUnitOfWork> _mockUow;
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly GetAllProductHandler _handler;
    private readonly RepositoryUtils _sut;

    public GetAllProductHandlerTests()
    {
        this._sut = new RepositoryUtils();
        this._mockProductRepository = new Mock<IProductRepository>();
        this._mockUow = new Mock<IUnitOfWork>();
        this._mockUow.Setup(u => u.ProductRepository).Returns(this._mockProductRepository.Object);
        this._handler = new GetAllProductHandler(this._mockUow.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnODataResponse()
    {
        // Arrange
        var productCount = 10;
        var queryOptions = TestUtils.ODataQueryOptionsUtils<Product>(productCount);
        var products = await this._sut.CreateQueryableMockProducts(productCount);

        // Act
        this._mockUow.Setup(r => r.ProductRepository.GetAll()).Returns(products);
        var result = await this._handler.Handle(new GetAllProductCommand(queryOptions), It.IsAny<CancellationToken>());

        // Assert
        var response = result.Value;
        Assert.Equal(productCount
            , response.Count);
        Assert.Equal(products, response.Value);
    }

    [Fact]
    public async Task Handle_ShouldReturnODataResponseWithNoProducts()
    {
        // Arrange
        var productCount = 0;
        var queryOptions = TestUtils.ODataQueryOptionsUtils<Product>(productCount);
        var products = await this._sut.CreateQueryableMockProducts(productCount);

        // Act
        this._mockUow.Setup(r => r.ProductRepository.GetAll()).Returns(products);
        var result = await this._handler.Handle(new GetAllProductCommand(queryOptions), It.IsAny<CancellationToken>());

        // Assert
        var response = result.Value;
        Assert.Equal(productCount
            , response.Count);
        Assert.Equal(products, response.Value);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenSelectQueryIsIncluded()
    {
        // Arrange
        var productCount = 10;
        var queryOptions = TestUtils.ODataQueryOptionsUtils<Product>(productCount, includeSelectQuery: true);
        var products = await this._sut.CreateQueryableMockProducts(productCount);

        // Act
        this._mockUow.Setup(r => r.ProductRepository.GetAll()).Returns(products);
        var result = await this._handler.Handle(new GetAllProductCommand(queryOptions), It.IsAny<CancellationToken>());

        // Assert
        Assert.True(result.IsError);
    }

    public void Dispose()
    {
        this._sut.Dispose();
        GC.SuppressFinalize(this);
    }

    ~GetAllProductHandlerTests()
    {
        this.Dispose();
    }
}