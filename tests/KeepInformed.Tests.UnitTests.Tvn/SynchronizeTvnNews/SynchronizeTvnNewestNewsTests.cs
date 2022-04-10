using KeepInformed.Application.Tvn.Commands.SynchronizeTvnNewestNews;
using KeepInformed.Application.Tvn.Repositories;
using KeepInformed.Application.Tvn.Services;
using KeepInformed.Contracts.Tvn.Commands.SynchronizeTvnNewestNews;
using KeepInformed.Contracts.Tvn.Dto;
using KeepInformed.Tests.UnitTests.Tvn.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KeepInformed.Tests.UnitTests.Tvn.SynchronizeTvnNews;

[TestClass]
public class SynchronizeTvnNewestNewsTests
{
    private ITvnNewsRepository _tvnNewsRepository;

    [TestInitialize]
    public void Initialize()
    {
        _tvnNewsRepository = new TvnNewsRepositoryMock();
    }

    [TestMethod]
    public async Task Test()
    {
        // ARRANGE
        var tvnNews1Date = DateTime.Parse("2022-01-01");
        var tvnNews1 = new TvnNewsDto()
        {
            Description = "Description",
            Guid = "TvnNews1",
            ImageUrl = "ImageUrl",
            PublicationDate = tvnNews1Date,
            Title = "Title",
            Url = "Url"
        };

        var tvnNews2Date = DateTime.Parse("2021-01-01");
        var tvnNews2 = new TvnNewsDto()
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
            .ReturnsAsync(new List<TvnNewsDto>() { tvnNews1, tvnNews2 });

        var command = new SynchronizeTvnNewestNewsCommand();
        var handler = new SynchronizeTvnNewestNewsCommandHandler(_tvnNewsRepository, tvnRssServiceMock.Object);

        // ACT
        await handler.Handle(command, new CancellationToken());

        // ASSERT
        Assert.AreEqual(2, _tvnNewsRepository.GetAll().Count());

        var news1 = await _tvnNewsRepository.GetByTvnGuid(tvnNews1.Guid);

        Assert.IsNotNull(news1);
        Assert.AreEqual(tvnNews1.Description, news1.TvnDescription);
        Assert.AreEqual(tvnNews1.Title, news1.TvnTitle);
        Assert.AreEqual(tvnNews1.Url, news1.TvnUrl);
        Assert.AreEqual(tvnNews1.ImageUrl, news1.TvnImageUrl);
        Assert.AreEqual(tvnNews1.PublicationDate, news1.TvnPublicationDate);
        Assert.AreNotEqual(default, news1.Id);

        var news2 = await _tvnNewsRepository.GetByTvnGuid(tvnNews2.Guid);

        Assert.IsNotNull(news2);
        Assert.AreEqual(tvnNews2.Description, news2.TvnDescription);
        Assert.AreEqual(tvnNews2.Title, news2.TvnTitle);
        Assert.AreEqual(tvnNews2.Url, news2.TvnUrl);
        Assert.AreEqual(tvnNews2.ImageUrl, news2.TvnImageUrl);
        Assert.AreEqual(tvnNews2.PublicationDate, news2.TvnPublicationDate);
        Assert.AreNotEqual(default, news2.Id);
    }
}
