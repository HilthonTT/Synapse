﻿using Infrastructure.Constants;
using Infrastructure.Database.Interceptors;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Modules.Posts.Application.Abstractions.Data;
using Modules.Posts.Domain.Comments;
using Modules.Posts.Domain.Likes;
using Modules.Posts.Domain.Posts;
using Modules.Posts.Infrastructure.Database;
using Modules.Posts.Infrastructure.Repositories;

namespace Modules.Posts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPostsInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.TryAddSingleton<UpdateAuditableEntitiesInterceptor>();

        string connectionString = configuration.GetConnectionStringOrThrow(ConnectionStringNames.Database);

        services.AddDbContext<PostsDbContext>((sp, options) =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Posts);
            });

            options.UseSnakeCaseNamingConvention();

            options.AddInterceptors(
                sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>());
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<PostsDbContext>());

        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ILikeRepository, LikeRepository>();

        return services;
    }
}