
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RetailPortal.Api.Controllers.Common;
using RetailPortal.Application.Auth.Commands;
using RetailPortal.Application.Auth.Queries;
using RetailPortal.Shared.DTOs.Auth;

namespace RetailPortal.Api.Controllers;

[ApiVersion("0.0")]
[ApiController]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController(ISender mediator, IMapper mapper) : ODataBaseController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await mediator.Send(mapper.Map<RegisterCommand>(request));

        return result.Match(
            authResult => this.Ok(mapper.Map<AuthResponse>(authResult)),
            this.Problem
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await mediator.Send(mapper.Map<LoginQuery>(request));

        return result.Match(
            authResult => this.Ok(mapper.Map<AuthResponse>(authResult)),
            this.Problem
        );
    }
}