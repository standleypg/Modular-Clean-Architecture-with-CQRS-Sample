using ErrorOr;
using MediatR;
using RetailPortal.Application.Common;
using RetailPortal.Domain.Entities;
using RetailPortal.Shared.DTOs.Product;

namespace RetailPortal.Application.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    PriceCommand Price,
    int Quantity
) : IRequireTransaction, IRequest<ErrorOr<CreateProductResponse>>;

public record PriceCommand(decimal Value, string Currency);