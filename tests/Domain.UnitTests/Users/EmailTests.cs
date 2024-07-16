using FluentAssertions;
using Modules.Users.Domain.Users;
using SharedKernel;

namespace Domain.UnitTests.Users;

public sealed class EmailTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Email_Should_ReturnFailure_WhenValueIsEmpty(string? value)
    {
        // Act
        Result<Email> emailResult = Email.Create(value);

        // Assert
        emailResult.IsFailure.Should().BeTrue();
    }

    [Theory]
    [InlineData("test.com")]
    public void Email_Should_ReturnFailure_WhenValueIsNotValidEmail(string? value)
    {
        // Act
        Result<Email> emailResult = Email.Create(value);

        // Assert
        emailResult.IsFailure.Should().BeTrue();
    }

    [Theory]
    [InlineData("test@test.com")]
    public void Email_Should_ReturnSuccess_WhenValueIsValidEmail(string? value)
    {
        // Act
        Result<Email> emailResult = Email.Create(value);

        // Assert
        emailResult.IsSuccess.Should().BeTrue();
    }
}
