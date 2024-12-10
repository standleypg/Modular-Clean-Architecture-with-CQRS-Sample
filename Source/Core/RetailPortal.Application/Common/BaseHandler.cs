using RetailPortal.Domain.Interfaces.Infrastructure.Data.UnitOfWork;

namespace RetailPortal.Application.Common;

public class BaseHandler(IUnitOfWork uow)
{
    protected readonly IUnitOfWork Uow = uow;
}