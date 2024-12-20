using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailPortal.Api.Controllers.Common;
using RetailPortal.Application.Auth.Commands.RegisterCommand;
using RetailPortal.Application.Auth.Commands.TokenExchange;
using RetailPortal.Application.Auth.Queries;
using RetailPortal.Shared.Constants;
using RetailPortal.Shared.DTOs.Auth;

namespace RetailPortal.Api.Controllers;

[ApiVersion("0.0")]
[ApiController]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController(ISender mediator, IMapper mapper) : ODataBaseController
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await mediator.Send(mapper.Map<RegisterCommand>(request));

        return result.Match(
            authResult => this.Ok(mapper.Map<AuthResponse>(authResult)),
            this.Problem
        );
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await mediator.Send(mapper.Map<LoginQuery>(request));

        return result.Match(
            authResult => this.Ok(mapper.Map<AuthResponse>(authResult)),
            this.Problem
        );
    }

    [HttpGet("token-exchange")]
    [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
    [Authorize(AuthenticationSchemes = Appsettings.AzureAdSettings.JwtBearerScheme)]
    public async Task<IActionResult> TokenExchange()
    {
        var result = await mediator.Send(mapper.Map<TokenExchangeCommand>(this.User));

        return result.Match(
            authResult => this.Ok(mapper.Map<AuthResponse>(authResult)),
            this.Problem
        );
    }
}