using ErrorOr;
using MediatR;
using RetailPortal.Application.Common;
using RetailPortal.Domain.Entities;
using RetailPortal.Domain.Interfaces.Infrastructure.Data.UnitOfWork;
using RetailPortal.Shared.DTOs.Common;

namespace RetailPortal.Application.Products.Queries.GetAllProduct;

public class GetAllProductHandler(IUnitOfWork uow)
    : BaseHandler(uow), IRequestHandler<GetAllProductCommand, ErrorOr<ODataResponse<Product>>>
{
    public async Task<ErrorOr<ODataResponse<Product>>> Handle(GetAllProductCommand request,
        CancellationToken cancellationToken)
    {
        var options = request.options;

        var products = this.Uow.ProductRepository.GetAll();

        var oDataResponse = await products.GetODataResponseAsync(options, cancellationToken);

        return oDataResponse;
    }
}