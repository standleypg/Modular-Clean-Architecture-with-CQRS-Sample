using AutoMapper;
using RetailPortal.Application.Users.Commands.CreateUser;
using RetailPortal.Domain.Entities;
using RetailPortal.Shared.DTOs;
using RetailPortal.Shared.DTOs.User;

namespace RetailPortal.Api.Common.Mapping;

public class UserMappingConfig : Profile
{
    public UserMappingConfig()
    {
        this.CreateMap<CreateUserRequest, CreateUserCommand>();

        this.CreateMap<User, UserResponse>()
            .ConstructUsing(user => new UserResponse(user.Id, $"{user.FirstName} {user.LastName}", user.Email));
    }
}