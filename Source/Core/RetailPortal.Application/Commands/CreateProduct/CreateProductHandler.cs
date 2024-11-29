using MediatR;
using RetailPortal.Application.Common;
using RetailPortal.Core.Entities;
using RetailPortal.Core.Entities.Common.Base;
using RetailPortal.Core.Entities.Common.ValueObjects;
using RetailPortal.Core.Interfaces.UnitOfWork;

namespace RetailPortal.Application.Commands.CreateProduct;

public class CreateProductHandler(IUnitOfWork uow) : BaseHandler(uow), IRequestHandler<CreateProductCommand, Result<Product>>
{
    public async Task<Result<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var price = Price.Create(request.Price.Value, request.Price.Currency);

        if (!price.IsSuccess)
        {
            return Result<Product>.Failure(price.Error!);
        }

        var product = Product.Create(request.Name, request.Description, price.Value, request.Quantity, null);

        if (!product.IsSuccess)
        {
            return Result<Product>.Failure(product.Error!);
        }

        var result = await this.Uow.ProductRepository.AddAsync(product.Value, cancellationToken);

        if (!result.IsSuccess)
        {
            return Result<Product>.Failure(result.Error!);
        }

        return Result<Product>.Success(result.Value);
    }
}