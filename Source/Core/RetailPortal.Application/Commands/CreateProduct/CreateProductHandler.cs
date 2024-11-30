using AutoMapper;
using ErrorOr;
using MediatR;
using RetailPortal.Application.Common;
using RetailPortal.Core.Interfaces.UnitOfWork;
using RetailPortal.Domain.Entities;
using RetailPortal.Domain.Entities.Common.ValueObjects;
using RetailPortal.Shared.DTOs.Product;

namespace RetailPortal.Application.Commands.CreateProduct;

public class CreateProductHandler(IUnitOfWork uow, IMapper mapper) : BaseHandler(uow), IRequestHandler<CreateProductCommand, ErrorOr<CreateProductResponse>>
{
    public async Task<ErrorOr<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var price = Price.Create(request.Price.Value, request.Price.Currency);

        var product = Product.Create(request.Name, request.Description, price, request.Quantity, null);

        var result = await this.Uow.ProductRepository.AddAsync(product, cancellationToken);

        return mapper.Map<CreateProductResponse>(result);
    }
}