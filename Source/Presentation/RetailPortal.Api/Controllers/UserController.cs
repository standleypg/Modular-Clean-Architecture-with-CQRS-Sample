using MediatR;
using Microsoft.AspNetCore.Mvc;
using RetailPortal.Api.Controllers.Common;
using RetailPortal.Application.Commands;
using RetailPortal.Application.Commands.AddUser;
using RetailPortal.Shared.DTOs;

namespace RetailPortal.Api.Controllers;

[ApiController]
[Route("api/v0.0/users")]
public class UserController(ISender sender): BaseController
{
    /// <summary>
    /// POST: api/v0.0/users
    /// TODO: Implement Automapper for mapping DTOs to Entities
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateUserAsync([FromBody] AddUserRequest user)
    {
        var result = await sender.Send(new AddUserCommand(user.FirstName, user.LastName, user.Email, user.Password));

        return result.Match(
            this.Ok,
            this.Problem
        );
    }
}