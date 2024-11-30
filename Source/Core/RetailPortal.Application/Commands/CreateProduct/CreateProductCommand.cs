using ErrorOr;
using MediatR;
using RetailPortal.Application.Common;
using RetailPortal.Core.Entities;

namespace RetailPortal.Application.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    PriceCommand Price,
    int Quantity
) : IRequireTransaction, IRequest<ErrorOr<Product>>;

public record PriceCommand(decimal Value, string Currency);