using MediatR;
using RetailPortal.Application.Common;
using RetailPortal.Core.Entities;
using RetailPortal.Core.Entities.Common.Base;

namespace RetailPortal.Application.Commands.AddUser;

public record AddUserCommand(string FirstName, string LastName, string Email, string Password): IRequireTransaction, IRequest<Result<User>>;