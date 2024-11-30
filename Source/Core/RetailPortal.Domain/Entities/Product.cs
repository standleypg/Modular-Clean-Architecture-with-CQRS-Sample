using RetailPortal.Domain.Entities.Common.Base;
using RetailPortal.Domain.Entities.Common.ValueObjects;

namespace RetailPortal.Domain.Entities;

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

    public static Product Create(string name, string description, Price price, int quantity, string? imageUrl)
    {
        return new Product(name, description, price, quantity, imageUrl);
    }
}