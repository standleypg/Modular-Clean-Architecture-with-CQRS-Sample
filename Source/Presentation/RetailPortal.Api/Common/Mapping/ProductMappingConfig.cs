using AutoMapper;
using RetailPortal.Application.Commands.CreateProduct;
using RetailPortal.Shared.DTOs;
using RetailPortal.Shared.DTOs.Product;

namespace RetailPortal.Api.Common.Mapping;

public class ProductMappingConfig : Profile
{
    public ProductMappingConfig()
    {
        this.CreateMap<CreateProductRequest, CreateProductCommand>();
    }
}