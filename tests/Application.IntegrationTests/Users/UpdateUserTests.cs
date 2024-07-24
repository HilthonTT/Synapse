using Application.IntegrationTests.Abstractions;
using FluentAssertions;
using Modules.Users.Application.Users.Create;
using Modules.Users.Application.Users.Update;
using SharedKernel;

namespace Application.IntegrationTests.Users;

public sealed class UpdateUserTests : BaseIntegrationTest
{
    public UpdateUserTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Handle_Should_UpdateUser_WhenCommandIsValidAndUserExists()
    {
        // Arrange
        Guid userId = await CreateUserAsync();

        var command = new UpdateUserCommand(
            userId,
            Faker.Internet.Email(),
            Faker.Internet.UserName(),
            Faker.Internet.UserName(),
            Faker.Internet.Url());

        // Act
        Result<Guid> result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    private async Task<Guid> CreateUserAsync()
    {
        var command = new CreateUserCommand(
            Guid.NewGuid().ToString(),
            Faker.Internet.Email(),
            Faker.Internet.UserName(),
            Faker.Internet.UserName(),
            Faker.Internet.Url());

        Result<Guid> result = await Sender.Send(command);

        return result.Value;
    }
}
