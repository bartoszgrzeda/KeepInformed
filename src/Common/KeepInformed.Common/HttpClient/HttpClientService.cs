namespace KeepInformed.Common.HttpClient;

public class HttpClientService : IHttpClientService
{
    public async Task<Stream> GetStreamFromUrl(string url, bool addDefaultUserAgentHeader = true)
    {
        using var httpClient = new System.Net.Http.HttpClient();

        if (addDefaultUserAgentHeader)
        {
            httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.28.4");
        }

        return await httpClient.GetStreamAsync(url);
    }
}
