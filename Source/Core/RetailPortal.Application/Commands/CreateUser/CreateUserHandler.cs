using AutoMapper;
using ErrorOr;
using MediatR;
using RetailPortal.Application.Commands.AddUser;
using RetailPortal.Application.Common;
using RetailPortal.Core.Interfaces.UnitOfWork;
using RetailPortal.Domain.Entities;
using RetailPortal.Shared.Constants;
using RetailPortal.Shared.DTOs.User;

namespace RetailPortal.Application.Commands.CreateUser;

public class CreateUserHandler(IUnitOfWork uow, IMapper mapper): BaseHandler(uow), IRequestHandler<CreateUserCommand, ErrorOr<CreateUserResponse>>
{
    public async Task<ErrorOr<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.FirstName, request.LastName,request.Email, request.Password);

        var userRole = await this.Uow.RoleRepository.GetAllAsync(cancellationToken);

        // For now we just set the user role to user as default
        var role = userRole.FirstOrDefault(x => x.Name == RolesList.User);

        user.AddRole(role!);

        var result = await this.Uow.UserRepository.AddAsync(user, cancellationToken);

        return mapper.Map<CreateUserResponse>(result);
    }
}