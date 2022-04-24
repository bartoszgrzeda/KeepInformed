﻿using KeepInformed.Common.HttpClient;
using KeepInformed.Infrastructure.Tvn.Services;
using MediatR;
using KeepInformed.Infrastructure.Tvn.Mappers;
using KeepInformed.Common.XmlDeserializer;
using KeepInformed.Infrastructure.DbAccess;
using KeepInformed.Application.News.Services.Tvn;
using KeepInformed.Application.News.Queries.GetNews;
using KeepInformed.Application.News.Repositories;
using KeepInformed.Infrastructure.DbAccess.Repositories;
using KeepInformed.Application.News.Mappers;
using KeepInformed.Web.Api.ResponseManager;
using KeepInformed.Infrastructure.MediatR.PipelineBehaviors;
using FluentValidation;
using KeepInformed.Contracts.News.Commands.MarkNewsAsSeen;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using KeepInformed.Application.Authorization.Repositories;
using KeepInformed.Application.Authorization.Services;
using KeepInformed.Application.Authorization.Commands.UserSignIn;
using KeepInformed.Contracts.Authorization.Commands.UserSignIn;
using KeepInformed.Application.Authorization.Mappers;

namespace KeepInformed.Web.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
    {
        services.AddTransient<IResponseManager, ResponseManager.ResponseManager>();

        services.AddTransient<IHttpClientService, HttpClientService>();
        services.AddTransient<IXmlDeserializer, XmlDeserializer>();

        services.AddTransient<INewsRepository, NewsRepository>();
        services.AddTransient<IUserRepository, UserRepository>();

        services.AddTransient<ITvnRssService, TvnRssService>();

        services.AddTransient<IEncrypter, Encrypter>();
        services.AddTransient<IJwtTokenService, JwtTokenService>();

        services.AddMemoryCache();

        return services;
    }

    public static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddMediatR(typeof(GetNewsQueryHandler)); // News module
        services.AddMediatR(typeof(UserSignInCommandHandler)); // Authorization module

        return services;
    }

    public static IServiceCollection RegisterValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(MarkNewsAsSeenCommandValidator)); // News module
        services.AddValidatorsFromAssemblyContaining(typeof(UserSignInCommandValidator)); // Authorization module

        return services;
    }

    public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(TvnItemProfile)); // Tvn infrastructure
        services.AddAutoMapper(typeof(NewsProfile)); // News module
        services.AddAutoMapper(typeof(UserProfile)); // Authorization module

        return services;
    }

    public static IServiceCollection RegisterDbContexts(this IServiceCollection services)
    {
        services.AddDbContext<KeepInformedContext>();

        return services;
    }

    public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.SaveToken = true;

            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            };
        });

        return services;
    }

    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new() { Title = "KeepInformed.Web.Api", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    }, new List<string>()
                }
            });
        });

        return services;
    }
}
