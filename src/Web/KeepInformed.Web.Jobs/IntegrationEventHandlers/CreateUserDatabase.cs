using KeepInformed.Common.EventHandlers;
using KeepInformed.Common.MultiTenancy;
using KeepInformed.Contracts.Authorization.Commands.UserCreateTenantDatabase;
using KeepInformed.Contracts.Authorization.IntegrationEvents;
using MediatR;

namespace KeepInformed.Web.Jobs.IntegrationEventHandlers;

public class CreateUserDatabase : IIntegrationEventHandler<UserConfirmedEmail>
{
    private readonly IMediator _mediator;
    private readonly ITenantProvider _tenantProvider;

    public CreateUserDatabase(IMediator mediator, ITenantProvider tenantProvider)
    {
        _mediator = mediator;
        _tenantProvider = tenantProvider;
    }

    public async Task Handle(UserConfirmedEmail integrationEvent)
    {
        var userId = integrationEvent.UserId;

        _tenantProvider.SetUserId(userId);

        var command = new UserCreateTenantDatabaseCommand()
        {
            UserId = userId
        };

        await _mediator.Send(command);
    }
}
