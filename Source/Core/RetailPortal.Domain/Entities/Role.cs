using RetailPortal.Core.Entities.Common.Base;
using System.Text.Json.Serialization;

namespace RetailPortal.Core.Entities;

public class Role : EntityBase
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

    public static Result<Role> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<Role>.Failure("Name must not be empty");

        if (string.IsNullOrWhiteSpace(description))
            return Result<Role>.Failure("Description must not be empty");

        return Result<Role>.Success(new Role(name, description));
    }
}