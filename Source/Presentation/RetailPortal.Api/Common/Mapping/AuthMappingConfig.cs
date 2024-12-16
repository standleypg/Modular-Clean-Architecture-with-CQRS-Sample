using AutoMapper;
using RetailPortal.Application.Auth.Commands;
using RetailPortal.Application.Auth.Common;
using RetailPortal.Application.Auth.Queries;
using RetailPortal.Shared.Constants;
using RetailPortal.Shared.DTOs.Auth;
using System.Security.Claims;

namespace RetailPortal.Api.Common.Mapping;

public class AuthMappingConfig : Profile
{
    public AuthMappingConfig()
    {
        this.CreateMap<RegisterRequest, RegisterCommand>();

        this.CreateMap<LoginRequest, LoginQuery>();

        this.CreateMap<AuthResult, AuthResponse>()
            .ConstructUsing(auth => new AuthResponse(auth.User.Id, auth.User.FirstName, auth.User.LastName, auth.User.Email, auth.Token));

        this.CreateMap<ClaimsPrincipal, TokenExchangeCommand>()
            .ConstructUsing(user => new TokenExchangeCommand(
                user.FindFirst(CustomClaimTypes.Email) != null ? user.FindFirst(CustomClaimTypes.Email)!.Value : user.FindFirst(ClaimTypes.Email)!.Value,
                user.FindFirst(CustomClaimTypes.Name)!.Value,
                user.FindFirst(CustomClaimTypes.Iss)!.Value
            ));
    }
}