using MediatR;
using RetailPortal.Application.Common;
using RetailPortal.Core.Entities;
using RetailPortal.Core.Entities.Common.Base;
using RetailPortal.Core.Interfaces.UnitOfWork;
using RetailPortal.Shared.Constants;

namespace RetailPortal.Application.Commands.AddUser;

public class AddUserHandler(IUnitOfWork uow): BaseHandler(uow), IRequestHandler<AddUserCommand, Result<User>>
{
    public async Task<Result<User>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.FirstName, request.LastName,request.Email, request.Password);

        if(!user.IsSuccess)
        {
            return Result<User>.Failure(user.Error!);
        }

        var userRole = await this.Uow.RoleRepository.GetAllAsync(cancellationToken);

        if(!userRole.IsSuccess)
        {
            return Result<User>.Failure(userRole.Error!);
        }

        // For now we just set the user role to user as default
        var role = userRole.Value.FirstOrDefault(x => x.Name == RolesList.User);

        user.Value.AddRole(role!);

        var result = await this.Uow.UserRepository.AddAsync(user.Value, cancellationToken);
        if(!result.IsSuccess)
        {
            return Result<User>.Failure(result.Error!);
        }

        return Result<User>.Success(user.Value);
    }
}