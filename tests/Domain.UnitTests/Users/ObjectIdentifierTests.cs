using FluentAssertions;
using Modules.Users.Domain.Users;
using SharedKernel;

namespace Domain.UnitTests.Users;

public sealed class ObjectIdentifierTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Name_Should_ReturnFailure_WhenValueIsInvalid(string? value)
    {
        // Act
        Result<ObjectIdentifier> oidResult = ObjectIdentifier.Create(value);

        // Assert
        oidResult.IsFailure.Should().BeTrue();
    }
}
