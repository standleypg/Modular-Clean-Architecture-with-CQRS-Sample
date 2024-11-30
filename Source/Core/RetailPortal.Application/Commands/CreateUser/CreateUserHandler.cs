using AutoMapper;
using ErrorOr;
using MediatR;
using RetailPortal.Application.Common;
using RetailPortal.Core.Interfaces.UnitOfWork;
using RetailPortal.Domain.Entities;
using RetailPortal.Shared.Constants;

namespace RetailPortal.Application.Commands.CreateUser;

public class CreateUserHandler(IUnitOfWork uow, IMapper mapper): BaseHandler(uow), IRequestHandler<CreateUserCommand, ErrorOr<User>>
{
    public async Task<ErrorOr<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.FirstName, request.LastName,request.Email, request.Password);

        var userRole = await this.Uow.RoleRepository.GetAllAsync(cancellationToken);

        // For now we just set the user role to user as default
        var role = userRole.FirstOrDefault(x => x.Name == RolesList.User);

        user.AddRole(role!);

        var result = await this.Uow.UserRepository.AddAsync(user, cancellationToken);

        return result;
    }
}