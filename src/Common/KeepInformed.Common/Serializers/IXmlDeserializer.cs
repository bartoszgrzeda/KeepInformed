namespace KeepInformed.Common.Serializers;

public interface IXmlDeserializer
{
    TExpectedResult? Deserialize<TExpectedResult>(string content) where TExpectedResult : class;
    TExpectedResult? Deserialize<TExpectedResult>(Stream content) where TExpectedResult : class;
}
