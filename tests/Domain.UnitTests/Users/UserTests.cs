using Modules.Users.Domain.Users;
using FluentAssertions;

namespace Domain.UnitTests.Users;

public sealed class UserTests
{
    [Fact]
    public void Create_Should_CreateUser_WhenNamesAreValid()
    {
        // Arrange
        ObjectIdentifier objectIdentifier = ObjectIdentifier.Create("random-oid").Value;
        Email email = Email.Create("test@test.com").Value;
        Name name = Name.Create("Full Name").Value;
        Username username = Username.Create("username").Value;

        // Act
        var user = User.Create(objectIdentifier, name, username, email, "image url");

        // Assert
        user.Should().NotBeNull();
    }

    [Fact]
    public void Create_Should_RaiseDomainEvent_WhenValuesAreValid()
    {
        // Arrange
        ObjectIdentifier objectIdentifier = ObjectIdentifier.Create("random-oid").Value;
        Email email = Email.Create("test@test.com").Value;
        Name name = Name.Create("Full Name").Value;
        Username username = Username.Create("username").Value;

        // Act
        var user = User.Create(objectIdentifier, name, username, email, "image url");

        // Assert
        user.DomainEvents
            .Should()
            .ContainSingle()
            .Which
            .Should().BeOfType<UserCreatedDomainEvent>();
    }
}
