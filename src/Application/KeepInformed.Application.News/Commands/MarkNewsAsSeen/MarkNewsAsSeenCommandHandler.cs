using KeepInformed.Application.News.Repositories;
using KeepInformed.Contracts.News.Commands.MarkNewsAsSeen;
using KeepInformed.Contracts.News.Exceptions;
using MediatR;

namespace KeepInformed.Application.News.Commands.MarkNewsAsSeen;

public class MarkNewsAsSeenCommandHandler : IRequestHandler<MarkNewsAsSeenCommand>
{
    private readonly INewsRepository _newsRepository;

    public MarkNewsAsSeenCommandHandler(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    public async Task<Unit> Handle(MarkNewsAsSeenCommand request, CancellationToken cancellationToken)
    {
        var newsId = request.NewsId;
        var news = await _newsRepository.Get(newsId);

        if (news == null)
        {
            throw new NewsNotFoundDomainException();
        }

        if (news.Seen)
        {
            return Unit.Value;
        }

        news.SetSeen(true);
        _newsRepository.Update(news);
        await _newsRepository.SaveChanges();

        return Unit.Value;
    }
}
