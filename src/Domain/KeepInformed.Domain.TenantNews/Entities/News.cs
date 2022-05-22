using KeepInformed.Common.Domain.Entities;
using KeepInformed.Contracts.TenantNews.Common;

namespace KeepInformed.Domain.TenantNews.Entities;

public class News : BaseEntity
{
    public string Title { get; private set; }
    public string Url { get; private set; }
    public string ImageUrl { get; private set; }
    public string Description { get; private set; }
    public DateTime PublicationDate { get; private set; }
    public NewsSource Source { get; private set; }
    public bool IsSeen { get; private set; }

    public News(Guid id, string title, string url, string imageUrl, string description, DateTime publicationDate, NewsSource source) : base(id)
    {
        Title = title;
        Url = url;
        ImageUrl = imageUrl;
        Description = description;
        PublicationDate = publicationDate;
        Source = source;
        IsSeen = false;
    }
}
