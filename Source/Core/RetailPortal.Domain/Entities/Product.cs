using RetailPortal.Core.Entities.Common.Base;
using RetailPortal.Core.Entities.Common.ValueObjects;

namespace RetailPortal.Core.Entities;

public class Product: EntityBase
{
    public string Name { get; private set;}
    public string Description { get; private set;}
    public Price Price { get; private set;}
    public int Quantity { get; private set;}
    public string? ImageUrl { get; private set;}
    public Guid? CategoryId { get; private set;}
    public Category? Category { get; private set;}
    public Guid? SellerId { get; private set;}
    public Seller? Seller { get; private set;}

    // Empty constructor for EF Core required when we have Value Objects in the entity
    private Product() { }

    private Product(string name, string description, Price price, int quantity, string? imageUrl)
    {
        this.Name = name;
        this.Description = description;
        this.Price = price;
        this.Quantity = quantity;
        this.ImageUrl = imageUrl;
    }

    public static Result<Product> Create(string name, string description, Price price, int quantity, string? imageUrl)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<Product>.Failure("Name must not be empty");

        if (string.IsNullOrWhiteSpace(description))
            return Result<Product>.Failure("Description must not be empty");

        if (quantity <= 0)
            return Result<Product>.Failure("Quantity must be greater than 0");

        return Result<Product>.Success(new Product(name, description, price, quantity, imageUrl));
    }
}