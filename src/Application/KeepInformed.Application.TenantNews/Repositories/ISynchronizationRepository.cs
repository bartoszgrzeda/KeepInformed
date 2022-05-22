using KeepInformed.Common.Domain.Repositories;
using KeepInformed.Contracts.TenantNews.Common;
using KeepInformed.Domain.TenantNews.Entities;

namespace KeepInformed.Application.TenantNews.Repositories;

public interface ISynchronizationRepository : IBaseRepository<Synchronization>
{
    Task<Synchronization?> GetLatesForNewsSourceByLatestNewsPublicationDate(NewsSource newsSource);
}
