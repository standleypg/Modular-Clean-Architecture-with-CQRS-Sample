using MediatR;
using RetailPortal.Application.Commands;
using RetailPortal.Application.Shared;
using RetailPortal.Core.Entities;
using RetailPortal.Core.Entities.Common.Base;
using RetailPortal.Core.Interfaces.UnitOfWork;

namespace RetailPortal.Application.Handlers;

public class AddUserHandler(IUnitOfWork uow): BaseHandler(uow), IRequestHandler<AddUserCommand, Result<User>>
{
    public async Task<Result<User>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        await this.Uow.UserRepository.AddAsync(request.user, cancellationToken);
        await this.Uow.CommitAsync();
        return Result<User>.Success(request.user);
    }
}