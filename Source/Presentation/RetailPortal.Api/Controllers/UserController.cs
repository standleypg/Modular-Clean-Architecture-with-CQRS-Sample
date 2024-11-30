using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RetailPortal.Api.Controllers.Common;
using RetailPortal.Application.Commands;
using RetailPortal.Application.Commands.AddUser;
using RetailPortal.Shared.DTOs;
using RetailPortal.Shared.DTOs.User;

namespace RetailPortal.Api.Controllers;

[ApiController]
[Route("api/v0.0/users")]
public class UserController(ISender sender, IMapper mapper): BaseController
{
    /// <summary>
    /// POST: api/v0.0/users
    /// TODO: Implement Automapper for mapping DTOs to Entities
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateUserAsync([FromBody] CreateUserRequest user)
    {
        var result = await sender.Send(mapper.Map<CreateUserCommand>(user));

        return result.Match(
            this.Ok,
            this.Problem
        );
    }
}