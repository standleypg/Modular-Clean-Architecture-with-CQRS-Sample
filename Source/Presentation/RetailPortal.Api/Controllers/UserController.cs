using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RetailPortal.Api.Controllers.Common;
using RetailPortal.Application.Users.Commands.CreateUser;
using RetailPortal.Shared.DTOs.User;

namespace RetailPortal.Api.Controllers;

[ApiVersion("0.0")]
[ApiController]
[Route("api/v{version:apiVersion}/users")]
public class UserController(ISender sender, IMapper mapper): ODataBaseController
{
    /// <summary>
    /// POST: api/v0.0/users
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
    {
        var result = await sender.Send(mapper.Map<CreateUserCommand>(request));

        return result.Match(
            user => this.Ok(mapper.Map<UserResponse>(user)),
            this.Problem
        );
    }
}