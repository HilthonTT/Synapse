using Microsoft.Extensions.DependencyInjection;
using Presentation.Extensions;

namespace Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddEndpoints();

        return services;
    }
}
