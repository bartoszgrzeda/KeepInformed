using KeepInformed.Application.News.Commands.MarkNewsAsSeen;
using KeepInformed.Application.News.Repositories;
using KeepInformed.Contracts.News.Commands.MarkNewsAsSeen;
using KeepInformed.Tests.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System;
using System.Threading;
using KeepInformed.Contracts.News.Exceptions;

namespace KeepInformed.Tests.UnitTests;

[TestClass]
public class MarkNewsAsSeenTests
{
    private INewsRepository _newsRepository;

    [TestInitialize]
    public async Task Initialize()
    {
        _newsRepository = new NewsRepositoryMock();
    }

    [TestMethod]
    public async Task MarkNewsAsSeen_ShouldWork()
    {
        // ARRANGE
        var newsId = Guid.NewGuid();
        await _newsRepository.Add(new Domain.News.Entities.News(newsId, string.Empty, string.Empty, string.Empty, string.Empty, DateTime.Now, false, Contracts.News.Common.NewsSource.Tvn, string.Empty));
        var handler = new MarkNewsAsSeenCommandHandler(_newsRepository);
        var command = new MarkNewsAsSeenCommand()
        {
            NewsId = newsId
        };

        // ACT
        await handler.Handle(command, new CancellationToken());

        // ASSERT
        var news = await _newsRepository.Get(newsId);
        Assert.IsTrue(news.Seen);
    }

    [TestMethod]
    public async Task MarkNewsAsSeen_ForNotExistingNews_ShouldThrow_NewsNotFoundDomainException()
    {
        // ARRANGE
        var handler = new MarkNewsAsSeenCommandHandler(_newsRepository);
        var command = new MarkNewsAsSeenCommand()
        {
            NewsId = Guid.NewGuid()
        };

        // ASSERT
        await Assert.ThrowsExceptionAsync<NewsNotFoundDomainException>(() => handler.Handle(command, new CancellationToken()));
    }
}
