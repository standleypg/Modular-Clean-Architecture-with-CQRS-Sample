using RetailPortal.Domain.Entities.Common.Base;
using System.Text.Json.Serialization;

namespace RetailPortal.Domain.Entities;

public sealed class Role : EntityBase
{
    public string Name { get; private set;}
    public string Description { get; private set;}
    [JsonIgnore]
    public ICollection<User> Users { get; private set;}

    private Role(string name, string description)
    {
        this.Name = name;
        this.Description = description;
        this.Users = new List<User>();
    }

    public static Role Create(string name, string description)
    {
        return new Role(name, description);
    }
}