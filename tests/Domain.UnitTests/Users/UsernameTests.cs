using FluentAssertions;
using Modules.Users.Domain.Users;
using SharedKernel;

namespace Domain.UnitTests.Users;

public sealed class UsernameTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Username_Should_ReturnFailure_WhenValueIsInvalid(string? value)
    {
        // Act
        Result<Username> usernameResult = Username.Create(value);

        // Assert
        usernameResult.IsFailure.Should().BeTrue();
    }
}
