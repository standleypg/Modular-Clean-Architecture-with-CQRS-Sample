using AutoMapper;
using RetailPortal.Application.Commands.AddUser;
using RetailPortal.Shared.DTOs;
using RetailPortal.Shared.DTOs.User;

namespace RetailPortal.Api.Common.Mapping;

public class UserMappingConfig : Profile
{
    public UserMappingConfig()
    {
        this.CreateMap<CreateUserRequest, CreateUserCommand>();
    }
}