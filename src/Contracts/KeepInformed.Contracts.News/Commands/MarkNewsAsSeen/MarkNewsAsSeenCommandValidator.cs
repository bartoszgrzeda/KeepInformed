using FluentValidation;

namespace KeepInformed.Contracts.News.Commands.MarkNewsAsSeen;

public class MarkNewsAsSeenCommandValidator : AbstractValidator<MarkNewsAsSeenCommand>
{
    public MarkNewsAsSeenCommandValidator()
    {
        RuleFor(x => x.NewsId)
            .NotEqual(default(Guid));            
    }
}
