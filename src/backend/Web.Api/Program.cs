using Application;
using Infrastructure;
using Presentation;
using Modules.Posts.Application;
using Modules.Posts.Infrastructure;
using Modules.Users.Application;
using Modules.Users.Infrastructure;
using Web.Api.Extensions;
using Presentation.Extensions;
using Asp.Versioning.ApiExplorer;
using Asp.Versioning.Builder;
using Asp.Versioning;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddUsersApplication()
    .AddPostsApplication()
    .AddInfrastructure(builder.Configuration)
    .AddUsersInfrastructure(builder.Configuration)
    .AddPostsInfrastructure(builder.Configuration)
    .AddPresentation();

WebApplication app = builder.Build();

ApiVersionSet apiVersionSet = app
    .NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

RouteGroupBuilder versionedGroup = app
    .MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(apiVersionSet);

app.MapEndpoints(versionedGroup);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        IReadOnlyList<ApiVersionDescription> descriptions = app.DescribeApiVersions();

        foreach (ApiVersionDescription description in descriptions)
        {
            string url = $"/swagger/{description.GroupName}/swagger.json";
            string name = description.GroupName.ToUpperInvariant();

            options.SwaggerEndpoint(url, name);
        }
    });

    app.ApplyMigration();
}

app.UseHttpsRedirection();

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseSerilogRequestLogging();

app.Run();
