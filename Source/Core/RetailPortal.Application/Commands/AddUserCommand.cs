using MediatR;
using RetailPortal.Application.Shared;
using RetailPortal.Core.Entities;
using RetailPortal.Core.Entities.Common.Base;

namespace RetailPortal.Application.Commands;

public record AddUserCommand(User user): IRequest<User>, IRequireTransaction, IRequest<Result<User>>;