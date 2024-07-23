using Api.FunctionalTests.Abstractions;
using FluentAssertions;
using Modules.Users.Api;
using Presentation.Contracts.Users;
using System.Net;
using System.Net.Http.Json;

namespace Api.FunctionalTests.Users;

public sealed class GetUserTests : BaseFunctionalTest
{
    public GetUserTests(FunctionalTestWebAppFactory factory) 
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        Guid userId = Guid.NewGuid();

        // Act
        HttpResponseMessage response = await HttpClient.GetAsync($"api/{ApiVersion}/users/{userId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Should_ReturnUser_WhenUserExists()
    {
        // Arrange
        Guid userId = await CreateUserAsync();

        // Act
        UserResponse? user = await HttpClient.GetFromJsonAsync<UserResponse>($"api/{ApiVersion}/users/{userId}");

        // Assert
        user.Should().NotBeNull();
    }

    private async Task<Guid> CreateUserAsync()
    {
        var request = new CreateUserRequest("test-oid", "test@test.com", "username", "name", "image");

        HttpResponseMessage response = await HttpClient.PostAsJsonAsync($"api/{ApiVersion}/users", request);

        return await response.Content.ReadFromJsonAsync<Guid>();
    }
}
