using MediatR;
using Microsoft.AspNetCore.Mvc;
using RetailPortal.Api.Controllers.Common;
using RetailPortal.Application.Commands.CreateProduct;
using RetailPortal.Shared.DTOs;

namespace RetailPortal.Api.Controllers;

[ApiController]
[Route("api/v0.0/products")]
public class ProductController(ISender sender) : BaseController
{
    // ! This isn't working yet as the we need to provide CategoryId
    // ! Which is not implemented yet
    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        var result = await sender.Send(new CreateProductCommand(
            request.Name,
            request.Description,
            new PriceCommand(request.Price.Value, request.Price.Currency),
            request.Quantity
        ));

        return result.Match(
            this.Ok,
            this.Problem
        );
    }
}