using ErrorOr;
using MediatR;
using RetailPortal.Application.Auth.Common;
using RetailPortal.Application.Common;
using System.Security.Claims;

namespace RetailPortal.Application.Auth.Commands;

public record TokenExchangeCommand(string Email, string Name, string TokenProvider) : IRequireTransaction, IRequest<ErrorOr<AuthResult>>;