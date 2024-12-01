namespace RetailPortal.Shared.DTOs.User;

public record UserResponse
(
    Guid Id,
    string FullName,
    string Email
);