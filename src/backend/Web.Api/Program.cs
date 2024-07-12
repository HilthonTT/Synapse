using Application;
using Infrastructure;
using Presentation;
using Modules.Posts.Application;
using Modules.Posts.Infrastructure;
using Modules.Users.Application;
using Modules.Users.Infrastructure;
using Web.Api.Extensions;
using Presentation.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

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

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigration();
}

app.UseHttpsRedirection();

app.Run();
