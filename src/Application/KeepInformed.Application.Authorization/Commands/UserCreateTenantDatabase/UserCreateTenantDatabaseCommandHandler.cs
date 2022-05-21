using KeepInformed.Application.Authorization.Repositories;
using KeepInformed.Common.MultiTenancy;
using KeepInformed.Contracts.Authorization.Commands.UserCreateTenantDatabase;
using KeepInformed.Contracts.Authorization.Exceptions;
using MediatR;

namespace KeepInformed.Application.Authorization.Commands.UserCreateTenantDatabase;

public class UserCreateTenantDatabaseCommandHandler : IRequestHandler<UserCreateTenantDatabaseCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ITenantDatabaseService _tenantDatabaseService;

    public UserCreateTenantDatabaseCommandHandler(IUserRepository userRepository, ITenantDatabaseService tenantDatabaseService)
    {
        _userRepository = userRepository;
        _tenantDatabaseService = tenantDatabaseService;
    }

    public async Task<Unit> Handle(UserCreateTenantDatabaseCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;

        var user = await _userRepository.Get(userId);

        if (user == null)
        {
            throw new UserWithProvidedIdNotFoundDomainException();
        }

        if (!user.IsEmailConfirmed)
        {
            throw new UserEmailNotConfirmedDomainException();
        }

        await _tenantDatabaseService.CreateUserTenantDatabaseIfNotExists();

        return Unit.Value;
    }
}
