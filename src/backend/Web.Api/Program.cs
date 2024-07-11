using Application;
using Infrastructure;
using Modules.Posts.Application;
using Modules.Posts.Infrastructure;
using Modules.Users.Application;
using Modules.Users.Infrastructure;
using Web.Api.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddUsersApplication()
    .AddPostsApplication()
    .AddInfrastructure(builder.Configuration)
    .AddUsersInfrastructure(builder.Configuration)
    .AddPostsInfrastructure(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigration();
}

app.UseHttpsRedirection();

app.Run();
