using Application.Abstractions.Data;
using Infrastructure.Constants;
using Infrastructure.Database;
using Infrastructure.Extensions;
using Infrastructure.Time;
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
}
