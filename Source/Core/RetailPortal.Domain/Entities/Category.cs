using RetailPortal.Core.Entities.Common.Base;
using System.Text.Json.Serialization;

namespace RetailPortal.Core.Entities;

public class Category: EntityBase
{
    public string Name { get; private set;}
    [JsonIgnore]
    public ICollection<Product> Products { get; private set;}

    private Category(string name)
    {
        this.Name = name;
        this.Products = new List<Product>();
    }

    public static Result<Category> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<Category>.Failure("Name must not be empty");

        return Result<Category>.Success(new Category(name));
    }
}