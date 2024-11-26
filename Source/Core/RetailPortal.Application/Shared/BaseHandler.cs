using RetailPortal.Core.Interfaces.UnitOfWork;

namespace RetailPortal.Application.Shared;

public class BaseHandler(IUnitOfWork uow)
{
    protected readonly IUnitOfWork Uow = uow;
}