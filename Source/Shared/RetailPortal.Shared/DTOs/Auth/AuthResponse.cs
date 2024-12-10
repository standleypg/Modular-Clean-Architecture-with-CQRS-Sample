namespace RetailPortal.Shared.DTOs.Auth;

public record AuthResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token
);