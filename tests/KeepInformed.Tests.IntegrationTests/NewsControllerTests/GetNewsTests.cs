using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace KeepInformed.Tests.IntegrationTests.NewsControllerTests;

[TestClass]
public class GetNewsTests
{
    private HttpClient _client;
    private string _url;

    [TestInitialize]
    public async Task Initialize()
    {
        var factory = new WebApplicationFactory<Program>();
        _client = factory.CreateClient();

        _url = "/GetNews";
    }

    [TestMethod]
    public async Task GetNews_ShouldWork()
    {
        // ACT
        var response = await _client.GetAsync(_url);

        // ASSERT
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
}
