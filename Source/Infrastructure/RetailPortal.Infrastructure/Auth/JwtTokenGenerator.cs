using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RetailPortal.Domain.Entities;
using RetailPortal.Domain.Entities.Common;
using RetailPortal.Domain.Interfaces.Infrastructure.Auth;
using RetailPortal.Domain.Interfaces.Infrastructure.Services;
using RetailPortal.Shared.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RetailPortal.Infrastructure.Auth;

public sealed class JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<Appsettings.JwtSettings> jwtOptions)
    : IJwtTokenGenerator
{
    private readonly Appsettings.JwtSettings _jwtSettings = jwtOptions.Value;

    public string GenerateToken(User user)
    {
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._jwtSettings.Secret)), SecurityAlgorithms.HmacSha512);

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var security = new JwtSecurityToken(
            issuer: nameof(TokenProvider.RetailPortalApp),
            audience: nameof(TokenProvider.RetailPortalApp),
            claims: claims,
            expires: dateTimeProvider.UtcNow.AddMinutes(this._jwtSettings.ExpirationMinutes),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(security);
    }
}