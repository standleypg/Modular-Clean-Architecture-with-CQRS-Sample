using MediatR;
using RetailPortal.Domain.Interfaces.UnitOfWork;

namespace RetailPortal.Application.Common;

public interface IRequireTransaction;

public class TransactionBehavior<TRequest, TResponse>(IUnitOfWork uow)
    : BaseHandler(uow), IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not IRequireTransaction)
        {
            return await next();
        }

        await this.Uow.BeginTransactionAsync();

        try
        {
            var response = await next();

            await this.Uow.CommitAsync();
            return response;
        }
        catch (Exception)
        {
            await this.Uow.RollbackAsync();
            throw;
        }
    }
}