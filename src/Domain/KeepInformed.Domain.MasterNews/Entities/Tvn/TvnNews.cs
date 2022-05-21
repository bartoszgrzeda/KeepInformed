using KeepInformed.Common.Domain.Entities;

namespace KeepInformed.Domain.MasterNews.Entities.Tvn;

public class TvnNews : BaseEntity
{
    public string Title { get; private set; }
    public string Url { get; private set; }
    public string ImageUrl { get; private set; }
    public string Description { get; private set; }
    public DateTime PublicationDate { get; private set; }
    public string Guid { get; private set; }

    public TvnNews(Guid id, string title, string url, string imageUrl, string description, DateTime publicationDate, string guid) : base(id)
    {
        Title = title;
        Url = url;
        ImageUrl = imageUrl;
        Description = description;
        PublicationDate = publicationDate;
        Guid = guid;
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
}
