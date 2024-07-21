﻿using Application.IntegrationTests.Abstractions;
using FluentAssertions;
using Modules.Users.Application.Users.Create;
using Modules.Users.Domain.Users;
using SharedKernel;

namespace Application.IntegrationTests.Users;

public sealed class CreateUserTests : BaseIntegrationTest
{
    public CreateUserTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Handle_Should_CreateUser_WhenCommandIsValid()
    {
        // Arrange
        var command = new CreateUserCommand(
            Guid.NewGuid().ToString(),
            Faker.Internet.Email(),
            Faker.Internet.UserName(),
            Faker.Internet.UserName(),
            Faker.Internet.Url());

        // Act
        Result<Guid> result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_AddUserToDatabase_WhenCommandIsValid()
    {
        // Arrange
        var command = new CreateUserCommand(
            Guid.NewGuid().ToString(),
            Faker.Internet.Email(),
            Faker.Internet.UserName(),
            Faker.Internet.UserName(),
            Faker.Internet.Url());

        // Act
        Result<Guid> result = await Sender.Send(command);

        // Assert
        User? user = await UsersDbContext.Users.FindAsync(result.Value);

        user.Should().NotBeNull();
    }
}