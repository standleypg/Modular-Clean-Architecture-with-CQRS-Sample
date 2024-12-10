using MediatR;
using ErrorOr;
using RetailPortal.Application.Auth.Common;
using RetailPortal.Application.Common;
using RetailPortal.Domain.Entities;

namespace RetailPortal.Application.Auth.Commands;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password): IRequireTransaction, IRequest<ErrorOr<AuthResult>>;