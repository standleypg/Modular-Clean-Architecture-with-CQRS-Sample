using RetailPortal.Domain.Entities;

namespace RetailPortal.Application.Auth.Common;

public record AuthResult(
    User User,
    string Token
);