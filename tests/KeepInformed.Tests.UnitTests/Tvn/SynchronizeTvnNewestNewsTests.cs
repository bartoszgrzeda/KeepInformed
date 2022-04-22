using KeepInformed.Application.News.Commands.Tvn.SynchronizeTvnNewestNews;
using KeepInformed.Application.News.Repositories;
using KeepInformed.Application.News.Services.Tvn;
using KeepInformed.Contracts.News.Commands.Tvn.SynchronizeTvnNewestNews;
using KeepInformed.Contracts.News.Common;
using KeepInformed.Contracts.News.Dto.Tvn;
using KeepInformed.Tests.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KeepInformed.Tests.UnitTests.Tvn;

[TestClass]
public class SynchronizeTvnNewestNewsTests
{
    private INewsRepository _newsRepository;

    [TestInitialize]
    public void Initialize()
    {
        _newsRepository = new NewsRepositoryMock();
    }

    [TestMethod]
    public async Task Test()
    {
        // ARRANGE
        var tvnNews1Date = DateTime.Parse("2022-01-01");
        var tvnNews1 = new TvnRssItemDto()
        {
            Description = "Description",
            Guid = "TvnNews1",
            ImageUrl = "ImageUrl",
            PublicationDate = tvnNews1Date,
            Title = "Title",
            Url = "Url"
        };

        var tvnNews2Date = DateTime.Parse("2021-01-01");
        var tvnNews2 = new TvnRssItemDto()
        {
            Description = "Description",
            Guid = "TvnNews2",
            ImageUrl = "ImageUrl",
            PublicationDate = tvnNews2Date,
            Title = "Title",
            Url = "Url"
        };

        var tvnRssServiceMock = new Mock<ITvnRssService>();
        tvnRssServiceMock.Setup(x => x.GetNewest())
            .ReturnsAsync(new List<TvnRssItemDto>() { tvnNews1, tvnNews2 });

        var command = new SynchronizeTvnNewestNewsCommand();
        var handler = new SynchronizeTvnNewestNewsCommandHandler(_newsRepository, tvnRssServiceMock.Object);

        // ACT
        await handler.Handle(command, new CancellationToken());

        // ASSERT
        Assert.AreEqual(2, _newsRepository.GetAll().Count());

        var news1 = await _newsRepository.GetBySourceAndCustomStringId(NewsSource.Tvn, tvnNews1.Guid);

        Assert.IsNotNull(news1);
        Assert.AreEqual(tvnNews1.Description, news1.Description);
        Assert.AreEqual(tvnNews1.Title, news1.Title);
        Assert.AreEqual(tvnNews1.Url, news1.Url);
        Assert.AreEqual(tvnNews1.ImageUrl, news1.ImageUrl);
        Assert.AreEqual(tvnNews1.PublicationDate, news1.PublicationDate);
        Assert.AreNotEqual(default, news1.Id);

        var news2 = await _newsRepository.GetBySourceAndCustomStringId(NewsSource.Tvn, tvnNews2.Guid);

        Assert.IsNotNull(news2);
        Assert.AreEqual(tvnNews2.Description, news2.Description);
        Assert.AreEqual(tvnNews2.Title, news2.Title);
        Assert.AreEqual(tvnNews2.Url, news2.Url);
        Assert.AreEqual(tvnNews2.ImageUrl, news2.ImageUrl);
        Assert.AreEqual(tvnNews2.PublicationDate, news2.PublicationDate);
        Assert.AreNotEqual(default, news2.Id);
    }
}
