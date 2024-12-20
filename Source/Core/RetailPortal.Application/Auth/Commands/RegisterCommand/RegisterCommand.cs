using ErrorOr;
using MediatR;
using RetailPortal.Application.Auth.Common;
using RetailPortal.Application.Common;

namespace RetailPortal.Application.Auth.Commands.RegisterCommand;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string? Password): IRequireTransaction, IRequest<ErrorOr<AuthResult>>;