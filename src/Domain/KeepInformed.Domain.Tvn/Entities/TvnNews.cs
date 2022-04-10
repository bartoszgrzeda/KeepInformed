using KeepInformed.Common.Domain.Entities;

namespace KeepInformed.Domain.Tvn.Entities;

public class TvnNews : BaseEntity
{
    public string TvnTitle { get; set; }
    public string TvnUrl { get; set; }
    public string TvnImageUrl { get; set; }
    public string TvnDescription { get; set; }
    public DateTime TvnPublicationDate { get; set; }
    public string TvnGuid { get; set; }

    protected TvnNews()
    {
    }

    public TvnNews(string title, string url, string imageUrl, string description, DateTime publicationDate, string guid)
    {
        TvnTitle = title;
        TvnUrl = url;
        TvnImageUrl = imageUrl;
        TvnDescription = description;
        TvnPublicationDate = publicationDate;
        TvnGuid = guid;
    }

    public void SetTvnTitle(string title)
    {
        if (TvnTitle == title)
        {
            return;
        }

        TvnTitle = title;
    }

    public void SetTvnUrl(string url)
    {
        if (TvnUrl == url)
        {
            return;
        }

        TvnUrl = url;
    }

    public void SetTvnImageUrl(string imageUrl)
    {
        if (TvnImageUrl == imageUrl)
        {
            return;
        }

        TvnImageUrl = imageUrl;
    }

    public void SetTvnDescription(string description)
    {
        if (TvnDescription == description)
        {
            return;
        }

        TvnDescription = description;
    }

    public void SetTvnPublicationDate(DateTime publicationDate)
    {
        if (TvnPublicationDate == publicationDate)
        {
            return;
        }

        TvnPublicationDate = publicationDate;
    }
}
