using FluentValidation;

namespace KeepInformed.Contracts.Authorization.Queries.GetUserJwt;

public class GetUserJwtQueryValidator : AbstractValidator<GetUserJwtQuery>
{
    public GetUserJwtQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
