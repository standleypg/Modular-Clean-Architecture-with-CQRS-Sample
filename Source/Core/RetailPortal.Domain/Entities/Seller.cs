using RetailPortal.Core.Entities.Common.Base;
using System.Text.Json.Serialization;

namespace RetailPortal.Core.Entities;

public class Seller: EntityBase
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

    public static Result<Seller> Create(string businessName)
    {
        if (string.IsNullOrWhiteSpace(businessName))
            return Result<Seller>.Failure("Business name must not be empty");

        return Result<Seller>.Success(new Seller(businessName));
    }
}