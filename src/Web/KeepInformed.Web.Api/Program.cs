using Autofac.Extensions.DependencyInjection;
using KeepInformed.Web.Api;
using KeepInformed.Web.Api.Middlewares;
using KeepInformed.Web.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.

// Shared
builder.Services.RegisterCustomServices()
    .RegisterMediatR()
    .RegisterAutoMapper()
    .RegisterDbContexts()
    .RegisterValidators()
    .RegisterRabbitMq();

// Web.Api services
builder.Services.AddJwtBearerAuthentication(builder.Configuration)
    .AddCustomSwagger();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<AuthMiddleware>();

app.MapControllers();

app.Run();
