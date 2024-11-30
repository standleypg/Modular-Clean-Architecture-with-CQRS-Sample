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

    public static Category Create(string name)
    {
        return new Category(name);
    }
}