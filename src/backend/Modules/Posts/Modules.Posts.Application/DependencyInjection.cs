using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Modules.Posts.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddPostsApplication(this IServiceCollection services)
    {
        Assembly assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        return services;
    }
}
