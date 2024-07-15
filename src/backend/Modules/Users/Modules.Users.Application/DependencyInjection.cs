using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Modules.Users.Domain.Followers;
using System.Reflection;

namespace Modules.Users.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddUsersApplication(this IServiceCollection services)
    {
        Assembly assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        services.AddScoped<IFollowerService, FollowerService>();

        return services;
    }
}
