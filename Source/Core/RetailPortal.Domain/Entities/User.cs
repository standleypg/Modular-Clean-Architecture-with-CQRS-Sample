using RetailPortal.Domain.Entities.Common.Base;
using System.Text.Json.Serialization;

namespace RetailPortal.Domain.Entities;

public sealed class User : EntityBase
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

    public static User Create(string firstName, string lastName, string email, string password)
    {
        return new User(firstName, lastName, email, password);
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
    public User AddSeller(Seller seller)
    {
        this.Seller = seller;
        return this;
    }

    public void Update(string? firstName = null, string? lastName = null, string? email = null, string? password = null)
    {
        if (!string.IsNullOrWhiteSpace(firstName))
        {
            this.FirstName = firstName;
        }

        if (!string.IsNullOrWhiteSpace(lastName))
        {
            this.LastName = lastName;
        }

        if (!string.IsNullOrWhiteSpace(email))
        {
            this.Email = email;
        }

        if (!string.IsNullOrWhiteSpace(password))
        {
            this.Password = password;
        }
    }
}