using AutoMapper;
using Microsoft.AspNetCore.OData.Query;
using RetailPortal.Application.Products.Commands.CreateProduct;
using RetailPortal.Application.Products.Queries.GetAllProduct;
using RetailPortal.Domain.Entities;
using RetailPortal.Shared.DTOs;
using RetailPortal.Shared.DTOs.Common;
using RetailPortal.Shared.DTOs.Product;

namespace RetailPortal.Api.Common.Mapping;

public class ProductMappingConfig : Profile
{
    public ProductMappingConfig()
    {
        this.CreateMap<CreateProductRequest, CreateProductCommand>();

           this.CreateMap<Product, ProductResponse>()
               .ConstructUsing(product => new ProductResponse(product.Id, product.Name, product.Description, new Price(product.Price.Value, product.Price.Currency), product.Quantity, product.ImageUrl, product.CategoryId, product.SellerId));

           this.CreateMap<ODataQueryOptions<Product>, GetAllProductCommand>()
               .ConstructUsing(options => new GetAllProductCommand(options));

           this.CreateMap<Domain.Entities.Common.ValueObjects.Price, Price>()
               .ConstructUsing(price => new Price(price.Value, price.Currency));

           this.CreateMap<ODataResponse<Product>, ODataResponse<ProductResponse>>()
               .ConstructUsing(products => new ODataResponse<ProductResponse>()
               {
                     Count = products.Count,
                     NextPage = products.NextPage,
                     Value = (products.Value ?? new List<Product>())
                         .Select(product => new ProductResponse(
                             product.Id,
                             product.Name,
                             product.Description,
                             new Price(product.Price.Value, product.Price.Currency),
                             product.Quantity,
                             product.ImageUrl,
                             product.CategoryId,
                             product.SellerId
                         )).ToList()
               });
    }
}