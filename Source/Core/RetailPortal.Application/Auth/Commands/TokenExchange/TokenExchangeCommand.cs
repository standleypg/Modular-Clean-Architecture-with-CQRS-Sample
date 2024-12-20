using ErrorOr;
using MediatR;
using RetailPortal.Application.Auth.Common;
using RetailPortal.Application.Common;

namespace RetailPortal.Application.Auth.Commands.TokenExchange;

public record TokenExchangeCommand(string Email, string Name, string TokenProvider) : IRequireTransaction, IRequest<ErrorOr<AuthResult>>;