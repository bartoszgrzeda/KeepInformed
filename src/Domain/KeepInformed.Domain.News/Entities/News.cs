using KeepInformed.Common.Domain.Entities;
using KeepInformed.Contracts.News.Common;

namespace KeepInformed.Domain.News.Entities;

public class News : BaseEntity
{
    public string Title { get; set; }
    public string Url { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public DateTime PublicationDate { get; set; }
    public bool Seen { get; set; }
    public NewsSource Source { get; set; }
    public string CustomStringId { get; set; }

    protected News()
    {
    }

    public News(string title, string url, string imageUrl, string description, DateTime publicationDate, bool seen, NewsSource source, string? customStringId) : base()
    {
        Title = title;
        Url = url;
        ImageUrl = imageUrl;
        Description = description;
        PublicationDate = publicationDate;
        Seen = seen;
        Source = source;
        CustomStringId = customStringId ?? Id.ToString();
    }

    public void SetTitle(string title)
    {
        if (Title == title)
        {
            return;
        }

        Title = title;
    }

    public void SetUrl(string url)
    {
        if (Url == url)
        {
            return;
        }

        Url = url;
    }

    public void SetImageUrl(string imageUrl)
    {
        if (ImageUrl == imageUrl)
        {
            return;
        }

        ImageUrl = imageUrl;
    }

    public void SetDescription(string description)
    {
        if (Description == description)
        {
            return;
        }

        Description = description;
    }

    public void SetPublicationDate(DateTime publicationDate)
    {
        if (PublicationDate == publicationDate)
        {
            return;
        }

        PublicationDate = publicationDate;
    }

    public void SetSeen(bool seen)
    {
        if (Seen == seen)
        {
            return;
        }

        Seen = seen;
    }
}
