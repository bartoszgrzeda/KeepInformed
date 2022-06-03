using Autofac.Extensions.DependencyInjection;
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
    .RegisterServiceBus();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
