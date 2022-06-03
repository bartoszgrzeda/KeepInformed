using Autofac.Extensions.DependencyInjection;
using KeepInformed.Web.Jobs;
using KeepInformed.Web.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.

// Shared
builder.Services.RegisterCustomServices()
    .RegisterMediatR()
    .RegisterAutoMapper()
    .RegisterDbContexts()
    .RegisterValidators();

// Web.Api services
builder.Services.RegisterIntegrationEventHandlers();

var app = builder.Build();

app.UseHttpsRedirection();

app.AddIntegrationEventsSubscriptions();

app.Run();