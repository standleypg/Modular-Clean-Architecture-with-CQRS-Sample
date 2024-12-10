using AutoMapper;
using MediatR;
using RetailPortal.Application.Auth.Common;
using RetailPortal.Application.Common;
using RetailPortal.Domain.Entities;
using RetailPortal.Domain.Interfaces.Infrastructure.Auth;
using RetailPortal.Domain.Interfaces.Infrastructure.Data.Repositories;
using RetailPortal.Domain.Interfaces.Infrastructure.Data.UnitOfWork;
using ErrorOr;
using RetailPortal.Domain.Common;
using RetailPortal.Domain.Entities.Common.ValueObjects;
using RetailPortal.Shared.Constants;

namespace RetailPortal.Application.Auth.Commands;

public class RegisterCommandHanlder(
    IUnitOfWork uow,
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordHasher passwordHasher) : BaseHandler(uow), IRequestHandler<RegisterCommand, ErrorOr<AuthResult>>
{
    public async Task<ErrorOr<AuthResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // Check user if already exists
        if (this.Uow.UserRepository.GetUserByEmail(command.Email) is not null)
            return Errors.User.DuplicateEmail();

        // Create user (generate id)
        passwordHasher.CreatePasswordHash(command.Password, out var passwordHash, out var passwordSalt);
        var password = Password.Create(passwordHash, passwordSalt);
        var user = User.Create(command.FirstName, command.LastName, command.Email, password);

        // For now we just set the user role to user level access as default
        var userRole = this.Uow.RoleRepository.GetAll();
        var role = userRole.FirstOrDefault(x => x.Name == RolesList.User);
        user.AddRole(role!);

        await this.Uow.UserRepository.AddAsync(user, cancellationToken);

        // Create JwtToken
        var token = jwtTokenGenerator.GenerateToken(user);

        return new AuthResult(user, token);
    }
}