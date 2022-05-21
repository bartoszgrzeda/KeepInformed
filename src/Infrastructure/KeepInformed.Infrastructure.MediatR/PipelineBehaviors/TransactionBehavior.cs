using KeepInformed.Infrastructure.MasterDbAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace KeepInformed.Infrastructure.MediatR.PipelineBehaviors;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly MasterKeepInformedDbContext _context;

    public TransactionBehavior(MasterKeepInformedDbContext context)
    {
        _context = context;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var currentTransaction = _context.Database.CurrentTransaction;

        if (currentTransaction != null)
        {
            return await next();
        }

        var response = default(TResponse);

        using (var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken))
        {
            try
            {
                response = await next();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        return response;
    }
}
