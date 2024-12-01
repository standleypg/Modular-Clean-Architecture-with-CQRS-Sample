using FluentValidation;

namespace RetailPortal.Application.Products.Queries.GetAllProduct;

public class GetAllProductCommandValidator: AbstractValidator<GetAllProductCommand>
{
    public GetAllProductCommandValidator()
    {
    }
}