using AutoMapper;
using RetailPortal.Domain.Entities;
using RetailPortal.Shared.DTOs.Product;

namespace RetailPortal.Application.Common.Mapping;

public class CreateProductMappingConfig: Profile
{
    public CreateProductMappingConfig()
    {
        this.CreateMap<CreateProductResponse, Product>()
            .ForMember(dest=>dest.Id, opt=>opt.MapFrom(src=>src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.SellerId));
    }
}