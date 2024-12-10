using ErrorOr;
using MediatR;
using RetailPortal.Application.Auth.Common;
using RetailPortal.Application.Common;

namespace RetailPortal.Application.Auth.Queries;

public record LoginQuery(string Email, string Password) : IRequireTransaction, IRequest<ErrorOr<AuthResult>>;