namespace RetailPortal.Shared.DTOs;

public record AddUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password
);