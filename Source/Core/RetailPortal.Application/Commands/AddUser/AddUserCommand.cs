

using MediatR;
using RetailPortal.Application.Common;
using ErrorOr;
using RetailPortal.Domain.Entities;

namespace RetailPortal.Application.Commands.AddUser;

public record AddUserCommand(string FirstName, string LastName, string Email, string Password): IRequireTransaction, IRequest<ErrorOr<User>>;