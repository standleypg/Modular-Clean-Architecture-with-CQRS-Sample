using ErrorOr;
using MediatR;
using RetailPortal.Application.Auth.Common;
using RetailPortal.Application.Common;
using RetailPortal.Domain.Entities;
using RetailPortal.Domain.Entities.Common;
using RetailPortal.Domain.Interfaces.Application.Services;
using RetailPortal.Domain.Interfaces.Infrastructure.Auth;
using RetailPortal.Domain.Interfaces.Infrastructure.Data.UnitOfWork;
using RetailPortal.Shared.Constants;

namespace RetailPortal.Application.Auth.Commands;

public class TokenExchangeHandler(
    IUnitOfWork uow,
    IRoleService roleService,
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordHasher passwordHasher)
    : BaseHandler(uow), IRequestHandler<TokenExchangeCommand, ErrorOr<AuthResult>>
{
    public async Task<ErrorOr<AuthResult>> Handle(TokenExchangeCommand request, CancellationToken cancellationToken)
    {
        var name = request.Name.AsSpan();
        var firstName = name[..name.IndexOf(' ')].ToString();
        var lastName = name[name.IndexOf(' ')..].ToString();
        var email = request.Email;

        if (this.Uow.UserRepository.GetUserByEmail(email) is not { } user)
        {
            var role = await roleService.GetRoleByNameAsync(Roles.User);
            var provider = request.TokenProvider == TokenProvider.Google.ToString()
                ? TokenProvider.Google
                : TokenProvider.Azure;
            user = User.Create(firstName, lastName, email, provider);
            user.AddRole(role);

            await this.Uow.UserRepository.AddAsync(user, cancellationToken);
        }

        var token = jwtTokenGenerator.GenerateToken(user);

        return new AuthResult(user, token);
    }
}