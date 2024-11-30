using AutoMapper;
using RetailPortal.Application.Commands.CreateUser;
using RetailPortal.Domain.Entities;
using RetailPortal.Shared.DTOs;
using RetailPortal.Shared.DTOs.User;

namespace RetailPortal.Api.Common.Mapping;

public class UserMappingConfig : Profile
{
    public UserMappingConfig()
    {
        this.CreateMap<CreateUserRequest, CreateUserCommand>();

        this.CreateMap<User, CreateUserResponse>()
            .ConstructUsing(user => new CreateUserResponse(user.Id, $"{user.FirstName} {user.LastName}", user.Email));
    }
}