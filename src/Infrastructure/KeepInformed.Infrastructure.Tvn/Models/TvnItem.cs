using System.Xml.Serialization;

namespace KeepInformed.Infrastructure.Tvn.Models;

[XmlRoot(ElementName = "item")]
public class TvnItem
{
	[XmlElement(ElementName = "title")]
	public string Title { get; set; }

	[XmlElement(ElementName = "link")]
	public string Link { get; set; }

	[XmlElement(ElementName = "description")]
	public string Description { get; set; }

	[XmlElement(ElementName = "pubDate")]
	public DateTime PubDate { get; set; }

	[XmlElement(ElementName = "guid")]
	public string Guid { get; set; }
}
