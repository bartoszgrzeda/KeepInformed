using System.Xml.Serialization;

namespace KeepInformed.Infrastructure.Tvn.Models;

[XmlRoot(ElementName = "channel")]
public class TvnChannel
{
	[XmlElement(ElementName = "title")]
	public string Title { get; set; }

	[XmlElement(ElementName = "description")]
	public string Description { get; set; }

	[XmlElement(ElementName = "link")]
	public string Link { get; set; }

	[XmlElement(ElementName = "language")]
	public string Language { get; set; }

	[XmlElement(ElementName = "copyright")]
	public string Copyright { get; set; }

	[XmlElement(ElementName = "lastBuildDate")]
	public DateTime LastBuildDate { get; set; }

	[XmlElement(ElementName = "item")]
	public List<TvnItem> Items { get; set; }
}