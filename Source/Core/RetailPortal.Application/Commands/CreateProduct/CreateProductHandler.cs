using ErrorOr;
using MediatR;
using RetailPortal.Application.Common;
using RetailPortal.Core.Entities;
using RetailPortal.Core.Entities.Common.ValueObjects;
using RetailPortal.Core.Interfaces.UnitOfWork;

namespace RetailPortal.Application.Commands.CreateProduct;

public class CreateProductHandler(IUnitOfWork uow) : BaseHandler(uow), IRequestHandler<CreateProductCommand, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var price = Price.Create(request.Price.Value, request.Price.Currency);

        var product = Product.Create(request.Name, request.Description, price, request.Quantity, null);

        var result = await this.Uow.ProductRepository.AddAsync(product, cancellationToken);

        return result;
    }
}