using AutoMapper;
using RetailPortal.Application.Auth.Commands;
using RetailPortal.Application.Auth.Common;
using RetailPortal.Application.Auth.Queries;
using RetailPortal.Shared.DTOs.Auth;

namespace RetailPortal.Api.Common.Mapping;

public class AuthMappingConfig : Profile
{
    public AuthMappingConfig()
    {
        this.CreateMap<RegisterRequest, RegisterCommand>();

        this.CreateMap<LoginRequest, LoginQuery>();

        this.CreateMap<AuthResult, AuthResponse>()
            .ConstructUsing(auth => new AuthResponse(auth.User.Id, auth.User.FirstName, auth.User.LastName, auth.User.Email, auth.Token));
    }
}