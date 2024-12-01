using RetailPortal.Domain.Entities.Common.Base;
using System.Text.Json.Serialization;

namespace RetailPortal.Domain.Entities;

public sealed class Seller: EntityBase
{
    public string? BusinessName { get; private set;}
    public Guid? UserId { get; private set;}
    public User? User { get; private set;}
    [JsonIgnore]
    public ICollection<Product> Products { get; private set;}

    private Seller(string businessName)
    {
        this.BusinessName = businessName;
        this.Products = new List<Product>();
    }

    public static Seller Create(string businessName)
    {
        return new Seller(businessName);
    }
}