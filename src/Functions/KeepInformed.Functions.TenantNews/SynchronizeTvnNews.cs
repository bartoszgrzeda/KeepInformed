using System.Threading.Tasks;
using KeepInformed.Common.MultiTenancy;
using KeepInformed.Contracts.TenantNews.Commands.SynchronizeTvnNews;
using KeepInformed.Contracts.TenantNews.IntegrationEvents;
using MediatR;
using Microsoft.Azure.WebJobs;

namespace KeepInformed.Functions.TenantNews
{
    public class SynchronizeTvnNews
    {
        private readonly IMediator _mediator;
        private readonly ITenantProvider _tenantProvider;

        public SynchronizeTvnNews(IMediator mediator, ITenantProvider tenantProvider)
        {
            _mediator = mediator;
            _tenantProvider = tenantProvider;
        }

        [FunctionName("SynchronizeTvnNews")]
        public async Task Run([ServiceBusTrigger("keepinformed-dev-topic", "SynchronizeTvnNews", Connection = "ConnectionString")] TvnNewsScheduledToBeSynchronized tvnNewsScheduledToBeSynchronized)
        {
            var userId = tvnNewsScheduledToBeSynchronized.UserId;

            _tenantProvider.SetUserId(userId);

            var command = new SynchronizeTvnNewsCommand();

            await _mediator.Send(command);
        }
    }
}
