using MediatR;

namespace KeepInformed.Common.MediatR;

public interface IMasterQuery<out TResponse> : IRequest<TResponse>
{
}
