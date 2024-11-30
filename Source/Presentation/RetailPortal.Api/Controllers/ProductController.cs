using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RetailPortal.Api.Controllers.Common;
using RetailPortal.Application.Commands.CreateProduct;
using RetailPortal.Shared.DTOs.Product;

namespace RetailPortal.Api.Controllers;

[ApiController]
[Route("api/v0.0/products")]
public class ProductController(ISender sender, IMapper mapper) : BaseController
{
    // ! This isn't working yet as the we need to provide CategoryId
    // ! Which is not implemented yet
    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        var result = await sender.Send(mapper.Map<CreateProductCommand>(request));

        return result.Match(
            product => this.Ok(mapper.Map<CreateProductResponse>(product)),
            this.Problem
        );
    }
}