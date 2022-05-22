using KeepInformed.Common.Domain.Entities;
using KeepInformed.Contracts.TenantNews.Common;

namespace KeepInformed.Domain.TenantNews.Entities;

public class Synchronization : BaseEntity
{
    public NewsSource NewsSource { get; private set; }
    public int NewsCount { get; private set; }
    public DateTime LatestNewsPublicationDate { get; private set; }

    public Synchronization(Guid id, NewsSource newsSource, int newsCount, DateTime latestNewsPublicationDate) : base(id)
    {
        NewsSource = newsSource;
        NewsCount = newsCount;
        LatestNewsPublicationDate = latestNewsPublicationDate;
    }
}
