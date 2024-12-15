using ErrorOr;
using MediatR;
using RetailPortal.Application.Auth.Common;
using RetailPortal.Application.Common;
using RetailPortal.Domain.Common;
using RetailPortal.Domain.Entities;
using RetailPortal.Domain.Interfaces.Infrastructure.Auth;
using RetailPortal.Domain.Interfaces.Infrastructure.Data.UnitOfWork;

namespace RetailPortal.Application.Auth.Queries;

public class LoginQueryHandler(IUnitOfWork uow, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
    : BaseHandler(uow), IRequestHandler<LoginQuery, ErrorOr<AuthResult>>
{
    public async Task<ErrorOr<AuthResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (this.Uow.UserRepository.GetUserByEmail(query.Email) is not { } user)
        {
            return Errors.Auth.InvalidCredentials();
        }

        if (!passwordHasher.VerifyPasswordHash(query.Password, user.Password.PasswordHash, user.Password.PasswordSalt))
        {
            return new[] { Errors.Auth.InvalidCredentials() }; // just an example of returning list of errors
        }

        var token = jwtTokenGenerator.GenerateToken(user);

        return new AuthResult(user, token);
    }
}