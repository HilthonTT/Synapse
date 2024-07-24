using Application.IntegrationTests.Abstractions;
using FluentAssertions;
using Modules.Posts.Application.Posts.Create;
using Modules.Posts.Domain.Posts;
using Modules.Users.Application.Users.Create;
using SharedKernel;

namespace Application.IntegrationTests.Posts;

public sealed class CreatePostTests : BaseIntegrationTest
{
    public CreatePostTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Handle_Should_CreatePost_WhenUserExists()
    {
        // Arrange
        var createdUser = new CreateUserCommand(
            Guid.NewGuid().ToString(), 
            Faker.Internet.Email(), 
            Faker.Internet.UserName(), 
            Faker.Internet.UserName(), 
            Faker.Internet.Url());

        Guid userId = (await Sender.Send(createdUser)).Value;

        var command = new CreatePostCommand(
            Guid.NewGuid(),
            userId,
            Faker.Lorem.Word(),
            Faker.Internet.Url(),
            Faker.Lorem.Sentence(),
            Faker.Lorem.Sentence());

        // Act
        Result<Guid> result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_AddPostToDatabase_WhenCommandIsValid()
    {
        // Arrange
        var createdUser = new CreateUserCommand(
            Guid.NewGuid().ToString(),
            Faker.Internet.Email(),
            Faker.Internet.UserName(),
            Faker.Internet.UserName(),
            Faker.Internet.Url());

        Guid userId = (await Sender.Send(createdUser)).Value;

        var command = new CreatePostCommand(
            Guid.NewGuid(),
            userId,
            Faker.Lorem.Word(),
            Faker.Internet.Url(),
            Faker.Lorem.Sentence(),
            Faker.Lorem.Sentence());

        // Act
        Result<Guid> result = await Sender.Send(command);

        // Assert
        Post? post = await PostsDbContext.Posts.FindAsync(result.Value);

        post.Should().NotBeNull();
    }
}
