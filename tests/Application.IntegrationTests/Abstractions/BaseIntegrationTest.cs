using Bogus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Modules.Posts.Infrastructure.Database;
using Modules.Users.Infrastructure.Database;

namespace Application.IntegrationTests.Abstractions;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
{
    private readonly IServiceScope _serviceScope;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _serviceScope = factory.Services.CreateScope();
        Sender = _serviceScope.ServiceProvider.GetRequiredService<ISender>();
        UsersDbContext = _serviceScope.ServiceProvider.GetRequiredService<UsersDbContext>();
        PostsDbContext = _serviceScope.ServiceProvider.GetRequiredService<PostsDbContext>();
        Faker = new Faker();
    }

    protected ISender Sender { get; init; }

    protected UsersDbContext UsersDbContext { get; init; }

    protected PostsDbContext PostsDbContext { get; init; }

    protected Faker Faker { get; init; }

    public void Dispose()
    {
        _serviceScope?.Dispose();
    }
}
