using KeepInformed.Common.Domain.Entities;
using KeepInformed.Contracts.TenantNews.Common;

namespace KeepInformed.Domain.TenantNews.Entities;

public class Synchronization : BaseEntity
{
    public DateTime Date { get; private set; }
    public NewsSource NewsSource { get; private set; }
    public int NewsCount { get; private set; }

    public Synchronization(Guid id, DateTime date, NewsSource newsSource, int newsCount) : base(id)
    {
        Date = date;
        NewsSource = newsSource;
        NewsCount = newsCount;
    }
}
