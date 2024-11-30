

using MediatR;
using RetailPortal.Application.Common;
using ErrorOr;
using RetailPortal.Domain.Entities;
using RetailPortal.Shared.DTOs.User;

namespace RetailPortal.Application.Commands.AddUser;

public record CreateUserCommand(string FirstName, string LastName, string Email, string Password): IRequireTransaction, IRequest<ErrorOr<CreateUserResponse>>;