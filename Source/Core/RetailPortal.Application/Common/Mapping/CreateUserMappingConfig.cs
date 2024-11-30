using AutoMapper;
using RetailPortal.Domain.Entities;
using RetailPortal.Shared.DTOs.User;

namespace RetailPortal.Application.Common.Mapping;

public class CreateUserMappingConfig: Profile
{
    public CreateUserMappingConfig()
    {
        this.CreateMap<CreateUserResponse, User>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest=>dest.Id, opt=>opt.MapFrom(src=>src.Id));
    }
}