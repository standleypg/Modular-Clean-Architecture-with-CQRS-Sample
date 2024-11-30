namespace RetailPortal.Shared.DTOs.User;

public record CreateUserResponse
{
    public Guid Id { get; init; }
    public string FullName { get; init; }
    public string Email { get; init; }

    // Parameterless constructor
    public CreateUserResponse() {}

    // Optional: Constructor with all parameters
    public CreateUserResponse(Guid id, string fullName, string email)
    {
        Id = id;
        FullName = fullName;
        Email = email;
    }
}