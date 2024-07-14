﻿using Application.Abstractions.Data;
using Application.Abstractions.Storage;
using Azure.Storage.Blobs;
using Infrastructure.Authentication;
using Infrastructure.Constants;
using Infrastructure.Database;
using Infrastructure.Extensions;
using Infrastructure.Storage;
using Infrastructure.Time;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using SharedKernel;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
       
        AddDatabase(services, configuration);
        AddHealthChecks(services, configuration);
        AddStorage(services, configuration);

        AddAuthentication(services);

        return services;
    }

    private static void AddDatabase(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionStringOrThrow(ConnectionStringNames.Database);

        services.AddSingleton<IDbConnectionFactory>(_ =>
            new DbConnectionFactory(new NpgsqlDataSourceBuilder(connectionString).Build()));
    }

    private static void AddHealthChecks(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionStringOrThrow(ConnectionStringNames.Database));
    }

    private static void AddStorage(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionStringOrThrow(ConnectionStringNames.BlobStorage);

        services.AddSingleton<IBlobService, BlobService>();
        services.AddSingleton(_ => new BlobServiceClient(connectionString));
    }

    private static void AddAuthentication(IServiceCollection services)
    {
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.AddAuthorization();
    }
}
