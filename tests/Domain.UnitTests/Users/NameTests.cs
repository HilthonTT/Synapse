using FluentAssertions;
using Modules.Users.Domain.Users;
using SharedKernel;

namespace Domain.UnitTests.Users;

public sealed class NameTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Name_Should_ReturnFailure_WhenValueIsInvalid(string? value)
    {
        // Act
        Result<Name> nameResult = Name.Create(value);

        // Assert
        nameResult.IsFailure.Should().BeTrue();
    }
}
