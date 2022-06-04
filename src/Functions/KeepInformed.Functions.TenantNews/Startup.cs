using KeepInformed.Web.Shared;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(KeepInformed.Functions.TenantNews.Startup))]

namespace KeepInformed.Functions.TenantNews;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.RegisterCustomServices()
            .RegisterMediatR()
            .RegisterAutoMapper()
            .RegisterDbContexts()
            .RegisterValidators()
            .RegisterServiceBus();
    }
}
