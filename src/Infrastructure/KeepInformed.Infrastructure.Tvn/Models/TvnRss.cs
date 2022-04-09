using System.Xml.Serialization;

namespace KeepInformed.Infrastructure.Tvn.Models;

[XmlRoot(ElementName = "rss")]
public class TvnRss
{
	[XmlElement(ElementName = "channel")]
	public TvnChannel Channel { get; set; }

	[XmlAttribute(AttributeName = "version")]
	public double Version { get; set; }

	[XmlAttribute(AttributeName = "atom")]
	public string Atom { get; set; }

	[XmlText]
	public string Text { get; set; }
}
