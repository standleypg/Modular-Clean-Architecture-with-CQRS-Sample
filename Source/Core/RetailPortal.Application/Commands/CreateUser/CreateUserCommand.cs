using ErrorOr;
using MediatR;
using RetailPortal.Application.Common;
using RetailPortal.Domain.Entities;

namespace RetailPortal.Application.Commands.CreateUser;

public record CreateUserCommand(string FirstName, string LastName, string Email, string Password): IRequireTransaction, IRequest<ErrorOr<User>>;