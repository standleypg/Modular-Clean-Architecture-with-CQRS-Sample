using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.OData.Query;
using RetailPortal.Domain.Entities;
using RetailPortal.Shared.DTOs.Common;

namespace RetailPortal.Application.Products.Queries.GetAllProduct;

public record GetAllProductCommand
    (
        ODataQueryOptions<Product>? options
    ) : IRequest<ErrorOr<ODataResponse<Product>>>;