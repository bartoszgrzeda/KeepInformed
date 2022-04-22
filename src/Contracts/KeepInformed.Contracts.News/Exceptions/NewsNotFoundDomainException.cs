using KeepInformed.Common.Exceptions;

namespace KeepInformed.Contracts.News.Exceptions;

public class NewsNotFoundDomainException : DomainException
{
    public NewsNotFoundDomainException() : base("NEWS_NOT_FOUND")
    {
    }
}
