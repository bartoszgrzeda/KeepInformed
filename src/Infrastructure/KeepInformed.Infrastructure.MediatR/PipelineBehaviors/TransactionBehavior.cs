using KeepInformed.Common.MediatR;
using KeepInformed.Infrastructure.MasterDbAccess;
using KeepInformed.Infrastructure.TenantDbAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace KeepInformed.Infrastructure.MediatR.PipelineBehaviors;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly MasterKeepInformedDbContext _masterContext;
    private readonly TenantKeepInformedDbContext _tenantContext;

    public TransactionBehavior(MasterKeepInformedDbContext masterContext, TenantKeepInformedDbContext tenantContext)
    {
        _masterContext = masterContext;
        _tenantContext = tenantContext;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var isMasterRequest = request is IMasterCommand || request is IMasterQuery<TResponse>;
        var isTenantRequest = request is ITenantCommand || request is ITenantQuery<TResponse>;

        DbContext context;

        if (isMasterRequest)
        {
            context = _masterContext;
        }
        else if (isTenantRequest)
        {
            context = _tenantContext;
        }
        else
        {
            throw new Exception("INVALID_REQUEST");
        }

        var currentTransaction = context.Database.CurrentTransaction;

        if (currentTransaction != null)
        {
            return await next();
        }

        var response = default(TResponse);

        using (var transaction = await context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken))
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
