using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Yarp.LoadBalancer.OpenApi;

internal sealed class ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider)
    : IConfigureNamedOptions<SwaggerGenOptions>
{
    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
        }
    }

    private static OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
    {
        var openApiInfo = new OpenApiInfo
        {
            Title = $"Synapse.Balancer v{description.ApiVersion}",
            Version = description.ApiVersion.ToString()
        };

        if (description.IsDeprecated)
        {
            openApiInfo.Description += " This Yarp Balancer version has been deprecated.";
        }

        return openApiInfo;
    }
}
