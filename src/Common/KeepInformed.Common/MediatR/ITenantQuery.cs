using MediatR;

namespace KeepInformed.Common.MediatR;

public interface ITenantQuery<out TResponse> : IRequest<TResponse>
{
}
