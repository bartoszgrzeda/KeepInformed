namespace KeepInformed.Contracts.News.Dto.Tvn;

public class TvnNewsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public DateTime PublicationDate { get; set; }
    public string Guid { get; set; }
}
