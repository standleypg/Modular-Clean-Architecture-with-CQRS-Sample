using AutoMapper;
using RetailPortal.Application.Products.Commands.CreateProduct;
using RetailPortal.Domain.Entities;
using RetailPortal.Shared.DTOs;
using RetailPortal.Shared.DTOs.Product;

namespace RetailPortal.Api.Common.Mapping;

public class ProductMappingConfig : Profile
{
    public ProductMappingConfig()
    {
        this.CreateMap<CreateProductRequest, CreateProductCommand>();

           this.CreateMap<Product, ProductResponse>()
               .ConstructUsing(product => new ProductResponse(product.Id, product.Name, product.Description, new Price(product.Price.Value, product.Price.Currency), product.Quantity, product.ImageUrl, product.CategoryId, product.SellerId));
    }
}