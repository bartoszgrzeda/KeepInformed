using MediatR;

namespace KeepInformed.Contracts.News.Commands.MarkNewsAsSeen;

public class MarkNewsAsSeenCommand : IRequest
{
    public Guid NewsId { get; set; }
}
