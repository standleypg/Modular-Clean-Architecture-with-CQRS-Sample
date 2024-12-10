using RetailPortal.Domain.Entities;

namespace RetailPortal.Domain.Interfaces.Infrastructure.Auth;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}