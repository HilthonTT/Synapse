using Api.FunctionalTests.Abstractions;
using Api.FunctionalTests.Contracts;
using Api.FunctionalTests.Extensions;
using FluentAssertions;
using Modules.Users.Application.Users;
using Presentation.Contracts.Users;
using System.Net;
using System.Net.Http.Json;

namespace Api.FunctionalTests.Users;

public sealed class CreateUserTests : BaseFunctionalTest
{
    public CreateUserTests(FunctionalTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenEmailIsMissing()
    {
        // Arrange
        var request = new CreateUserRequest("test-oid", "", "username", "name", "image");

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(UsersEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetailsAsync();

        problemDetails.Errors.Select(e => e.Code)
            .Should()
            .Contain([
                UserErrorCodes.CreateUser.MissingEmail,
                UserErrorCodes.CreateUser.InvalidEmail,
            ]);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenEmailIsInvalid()
    {
        // Arrange
        var request = new CreateUserRequest("test-oid", "test", "username", "name", "image");

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(UsersEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetailsAsync();

        problemDetails.Errors.Select(e => e.Code)
            .Should()
            .Contain([UserErrorCodes.CreateUser.InvalidEmail]);
    }

    [Fact]
    public async Task Should_ReturnOk_WhenRequestIsValid()
    {
        // Arrange
        var request = new CreateUserRequest("test-oid", "test@test.com", "username", "name", "image");

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(UsersEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_ReturnConflict_WhenUserExists()
    {
        // Arrange
        var request = new CreateUserRequest("test-oid", "test@test.com", "username", "name", "image");

        // Act
        await HttpClient.PostAsJsonAsync(UsersEndpoint, request);

        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(UsersEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenObjectIdentifierIsMissing()
    {
        // Arrange
        var request = new CreateUserRequest("", "test@test.com", "username", "name", "image");

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(UsersEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetailsAsync();

        problemDetails.Errors.Select(e => e.Code)
            .Should()
            .Contain([
                UserErrorCodes.CreateUser.MissingObjectIdentifier,
            ]);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenNameIsMissing()
    {
        // Arrange
        var request = new CreateUserRequest("test-oid", "test@test.com", "username", "", "image");

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(UsersEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetailsAsync();

        problemDetails.Errors.Select(e => e.Code)
            .Should()
            .Contain([
                UserErrorCodes.CreateUser.MissingName,
            ]);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenUsernameIsMissing()
    {
        // Arrange
        var request = new CreateUserRequest("test-oid", "test@test.com", "", "name", "image");

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(UsersEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetailsAsync();

        problemDetails.Errors.Select(e => e.Code)
            .Should()
            .Contain([
                UserErrorCodes.CreateUser.MissingUsername,
            ]);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenImageUrlIsMissing()
    {
        // Arrange
        var request = new CreateUserRequest("test-oid", "test@test.com", "username", "name", "");

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(UsersEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetailsAsync();

        problemDetails.Errors.Select(e => e.Code)
            .Should()
            .Contain([
                UserErrorCodes.CreateUser.MissingImageUrl,
            ]);
    }
}
