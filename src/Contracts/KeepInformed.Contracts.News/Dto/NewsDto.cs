using KeepInformed.Contracts.News.Common;

namespace KeepInformed.Contracts.News.Dto;

public class NewsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Url { get; set; }
    public bool Seen { get; set; }
    public NewsSource Source { get; set; }
    public DateTime PublicationDate { get; set; }
}
