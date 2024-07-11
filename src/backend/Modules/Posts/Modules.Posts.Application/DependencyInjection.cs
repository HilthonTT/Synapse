﻿using Microsoft.Extensions.DependencyInjection;

namespace Modules.Posts.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddPostsApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}
