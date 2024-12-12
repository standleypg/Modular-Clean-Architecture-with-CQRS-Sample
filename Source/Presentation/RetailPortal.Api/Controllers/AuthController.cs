
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = "Azure")]
    public async Task<IActionResult> TokenExchange()
    {
        await Task.CompletedTask;
        // TODO: Implement token exchange logic for Google and Azure AD
        // TODO: If the user is not yet registered, register the user
        // TODO: If the user is already registered, update the user's information and return a new token
        return this.Ok("Token exchange successful");
    }
}