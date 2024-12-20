using ErrorOr;
using MediatR;
using RetailPortal.Application.Auth.Common;
using RetailPortal.Application.Common;
using RetailPortal.Domain.Common;
using RetailPortal.Domain.Entities;
using RetailPortal.Domain.Entities.Common.ValueObjects;
using RetailPortal.Domain.Interfaces.Application.Services;
using RetailPortal.Domain.Interfaces.Infrastructure.Auth;
using RetailPortal.Domain.Interfaces.Infrastructure.Data.UnitOfWork;
using RetailPortal.Shared.Constants;

namespace RetailPortal.Application.Auth.Commands.RegisterCommand;

public class RegisterCommandHandler(
    IUnitOfWork uow,
    IRoleService roleService,
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordHasher passwordHasher) : BaseHandler(uow), IRequestHandler<RegisterCommand, ErrorOr<AuthResult>>
{
    public async Task<ErrorOr<AuthResult>> Handle(Commands.RegisterCommand.RegisterCommand command, CancellationToken cancellationToken)
    {
        if (this.Uow.UserRepository.GetUserByEmail(command.Email) is not null)
            return Errors.User.DuplicateEmail();

        passwordHasher.CreatePasswordHash(command.Password!, out var passwordHash, out var passwordSalt);
        var password = Password.Create(passwordHash, passwordSalt);
        var user = User.Create(command.FirstName, command.LastName, command.Email, password);

        var role = await roleService.GetRoleByNameAsync(Roles.User);
        user.AddRole(role);

        await this.Uow.UserRepository.AddAsync(user, cancellationToken);

        var token = jwtTokenGenerator.GenerateToken(user);

        return new AuthResult(user, token);
    }
}