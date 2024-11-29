using MediatR;
using RetailPortal.Application.Common;
using RetailPortal.Core.Entities;
using RetailPortal.Core.Entities.Common.Base;

namespace RetailPortal.Application.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    PriceCommand Price,
    int Quantity
) : IRequireTransaction, IRequest<Result<Product>>;

public record PriceCommand(decimal Value, string Currency);