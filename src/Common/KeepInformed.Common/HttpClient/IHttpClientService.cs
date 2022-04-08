namespace KeepInformed.Common.HttpClient;

public interface IHttpClientService
{
    Task<Stream> GetStreamFromUrl(string url, bool addDefaultUserAgentHeader = true);
}
