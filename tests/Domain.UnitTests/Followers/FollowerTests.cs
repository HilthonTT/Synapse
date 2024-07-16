using FluentAssertions;
using Modules.Users.Domain.Followers;
using Modules.Users.Domain.Users;

namespace Domain.UnitTests.Followers;

public sealed class FollowerTests
{
    private static readonly Email Email = Email.Create("test@test.com").Value;
    private static readonly Name Name = Name.Create("Full Name").Value;
    private static readonly Username Username = Username.Create("username").Value;
    private static readonly ObjectIdentifier Oid = ObjectIdentifier.Create("random-oid").Value;
    private static readonly DateTime UtcNow = DateTime.UtcNow;
    private const string ImageUrl = "image-url";

    [Fact]
    public void Create_Should_RaiseDomainEvent()
    {
        // Arrange
        var user = User.Create(Oid, Name, Username, Email, ImageUrl);
        var followed = User.Create(Oid, Name, Username, Email, ImageUrl);

        // Act
        var follower = Follower.Create(user.Id, followed.Id, UtcNow);

        // Assert
        follower.DomainEvents
            .Should()
            .ContainSingle()
            .Which
            .Should().BeOfType<FollowerCreatedDomainEvent>();
    }
}
