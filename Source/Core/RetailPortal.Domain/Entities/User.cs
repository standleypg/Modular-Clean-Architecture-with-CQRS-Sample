using RetailPortal.Core.Entities.Common.Base;
using RetailPortal.Core.Entities.Common.ValueObjects;
using RetailPortal.Core.Entities.Constants;
using System.Text.Json.Serialization;

namespace RetailPortal.Core.Entities;

public class User : EntityBase
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public Guid? SellerId { get; private set; }
    public Seller? Seller { get; private set; }

    [JsonIgnore] public ICollection<Address> Addresses { get; private set; }
    [JsonIgnore] public ICollection<Role> Roles { get; private set; }

    private User(string firstName, string lastName, string email, string password)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.Password = password;
        this.Addresses = new List<Address>();
        this.Roles = new List<Role>();
    }

    public static Result<User> Create(string firstName, string lastName, string email, string password)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result<User>.Failure("First name must not be empty");

        if (string.IsNullOrWhiteSpace(lastName))
            return Result<User>.Failure("Last name must not be empty");

        if (string.IsNullOrWhiteSpace(email))
            return Result<User>.Failure("Email must not be empty");

        if (string.IsNullOrWhiteSpace(password))
            return Result<User>.Failure("Password must not be empty");

        return Result<User>.Success(new User(firstName, lastName, email, password));
    }

    public void AddRole(Role role)
    {
        this.Roles.Add(role);
    }

    public void AddAddress(Address address)
    {
        this.Addresses.Add(address);
    }

    // Add seller only if the user have a role of seller
    public Result<User> AddSeller(Seller seller)
    {
        if (this.Roles.All(r => r.Name != RolesList.Seller))
        {
            return Result<User>.Failure("User must have a role of seller to add a seller");
        }

        this.Seller = seller;
        return Result<User>.Success(this);

    }
}