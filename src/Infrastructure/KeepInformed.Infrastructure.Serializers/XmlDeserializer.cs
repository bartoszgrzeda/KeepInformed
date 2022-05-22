using KeepInformed.Common.Serializers;
using System.Xml.Serialization;

namespace KeepInformed.Infrastructure.Serializers;

public class XmlDeserializer : IXmlDeserializer
{
    public TExpectedResult? Deserialize<TExpectedResult>(string content) where TExpectedResult : class
    {
        using var stream = new StringReader(content);

        var xmlSerializer = new XmlSerializer(typeof(TExpectedResult));
        var result = xmlSerializer.Deserialize(stream) as TExpectedResult;

        return result as TExpectedResult;
    }

    public TExpectedResult? Deserialize<TExpectedResult>(Stream content) where TExpectedResult : class
    {
        var xmlSerializer = new XmlSerializer(typeof(TExpectedResult));
        var result = xmlSerializer.Deserialize(content);

        return result as TExpectedResult;
    }
}
