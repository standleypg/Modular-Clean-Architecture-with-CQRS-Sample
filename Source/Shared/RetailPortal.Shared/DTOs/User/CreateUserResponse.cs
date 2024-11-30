namespace RetailPortal.Shared.DTOs.User;

public record CreateUserResponse
(
    Guid Id,
    string FullName,
    string Email
);