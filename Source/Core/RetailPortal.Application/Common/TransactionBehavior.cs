using MediatR;
using RetailPortal.Domain.Interfaces.Infrastructure.Data.UnitOfWork;

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

        await this.Uow.BeginTransactionAsync(cancellationToken);

        try
        {
            var response = await next();

            await this.Uow.CommitAsync(cancellationToken);
            return response;
        }
        catch (Exception)
        {
            await this.Uow.RollbackAsync(cancellationToken);
            throw;
        }
    }
}