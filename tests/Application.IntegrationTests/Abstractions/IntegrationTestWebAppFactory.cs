using Application.Abstractions.Data;
using Hangfire;
using Hangfire.PostgreSql;
using Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Modules.Posts.Infrastructure.Database;
using Modules.Users.Infrastructure.Database;
using Npgsql;
using Testcontainers.PostgreSql;
using Testcontainers.Redis;

namespace Application.IntegrationTests.Abstractions;

public sealed class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("synapse")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private readonly RedisContainer _redisContainer = new RedisBuilder()
        .WithImage("redis:latest")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddHangfire(config =>
                config.UsePostgreSqlStorage(
                    options => options.UseNpgsqlConnection(_dbContainer.GetConnectionString())));

            services.RemoveAll(typeof(IDbConnectionFactory));
            services.AddSingleton<IDbConnectionFactory>(_ =>
                new DbConnectionFactory(new NpgsqlDataSourceBuilder(_dbContainer.GetConnectionString()).Build()));

            services.RemoveAll(typeof(DbContextOptions<UsersDbContext>));
            services.AddDbContext<UsersDbContext>(options =>
                options
                    .UseNpgsql(_dbContainer.GetConnectionString())
                    .UseSnakeCaseNamingConvention());

            services.RemoveAll(typeof(DbContextOptions<PostsDbContext>));
            services.AddDbContext<PostsDbContext>(options =>
                 options
                    .UseNpgsql(_dbContainer.GetConnectionString())
                    .UseSnakeCaseNamingConvention());

            services.RemoveAll(typeof(RedisCacheOptions));
            services.AddStackExchangeRedisCache(redisCacheOptions => 
                redisCacheOptions.Configuration = _redisContainer.GetConnectionString());
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        await _redisContainer.StartAsync();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _redisContainer.StopAsync();
    }
}
