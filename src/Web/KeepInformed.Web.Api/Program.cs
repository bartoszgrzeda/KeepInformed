using KeepInformed.Web.Api;
using KeepInformed.Web.Shared;

var builder = WebApplication.CreateBuilder(args);

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

app.MapControllers();

app.Run();
