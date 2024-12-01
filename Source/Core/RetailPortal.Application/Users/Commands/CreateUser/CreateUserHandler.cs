using AutoMapper;
using ErrorOr;
using MediatR;
using RetailPortal.Application.Common;
using RetailPortal.Domain.Entities;
using RetailPortal.Domain.Interfaces.UnitOfWork;
using RetailPortal.Shared.Constants;

namespace RetailPortal.Application.Users.Commands.CreateUser;

public class CreateUserHandler(IUnitOfWork uow, IMapper mapper): BaseHandler(uow), IRequestHandler<CreateUserCommand, ErrorOr<User>>
{
    public async Task<ErrorOr<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.FirstName, request.LastName,request.Email, request.Password);

        var userRole = this.Uow.RoleRepository.GetAll();

        // For now we just set the user role to user as default
        var role = userRole.FirstOrDefault(x => x.Name == RolesList.User);

        user.AddRole(role!);

        var result = await this.Uow.UserRepository.AddAsync(user, cancellationToken);

        return result;
    }
}