namespace RetailPortal.Shared.DTOs.User;

public record CreateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password
);