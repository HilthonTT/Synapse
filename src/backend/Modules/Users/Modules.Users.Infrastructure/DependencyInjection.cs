﻿using Infrastructure.Constants;
using Infrastructure.Database.Interceptors;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Modules.Users.Application.Abstractions.Data;
using Modules.Users.Domain.Followers;
using Modules.Users.Domain.Users;
using Modules.Users.Infrastructure.Database;
using Modules.Users.Infrastructure.Repositories;

namespace Modules.Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddUsersInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.TryAddSingleton<UpdateAuditableEntitiesInterceptor>();

        string connectionString = configuration.GetConnectionStringOrThrow(ConnectionStringNames.Database);

        services.AddDbContext<UsersDbContext>(
            (sp, options) =>
            {
                options.UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users);
                });

                options.UseSnakeCaseNamingConvention();

                options.AddInterceptors(
                    sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>());
            });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFollowerRepository, FollowerRepository>();

        return services;
    }
}
