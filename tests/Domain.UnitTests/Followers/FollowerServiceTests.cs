using FluentAssertions;
using Modules.Users.Domain.Followers;
using Modules.Users.Domain.Users;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using SharedKernel;

namespace Domain.UnitTests.Followers;

public sealed class FollowerServiceTests
{
    private readonly IFollowerRepository _followerRepositoryMock;
    private readonly IFollowerService _followerService;

    private static readonly Email Email = Email.Create("test@test.com").Value;
    private static readonly Name Name = Name.Create("Full Name").Value;
    private static readonly Username Username = Username.Create("username").Value;
    private static readonly ObjectIdentifier Oid = ObjectIdentifier.Create("random-oid").Value;
    private static readonly DateTime UtcNow = DateTime.UtcNow;
    private const string ImageUrl = "image-url";

    public FollowerServiceTests()
    {
        _followerRepositoryMock = Substitute.For<IFollowerRepository>();

        IDateTimeProvider dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
        dateTimeProviderMock.UtcNow.Returns(UtcNow);

        _followerService = new FollowerService(dateTimeProviderMock, _followerRepositoryMock);
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnError_WhenFollowingSameUser()
    {
        // Arrange
        var user = User.Create(Oid, Name, Username, Email, ImageUrl);

        // Act
        Result<Follower> followerResult = await _followerService.StartFollowingAsync(user, user);

        // Assert
        followerResult.IsFailure.Should().BeTrue();
        followerResult.Error.Should().Be(FollowerErrors.SameUser);
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnError_WhenAlreadyFollowing()
    {
        // Arrange
        var user = User.Create(Oid, Name, Username, Email, ImageUrl);
        var followed = User.Create(Oid, Name, Username, Email, ImageUrl);

        _followerRepositoryMock
            .IsAlreadyFollowingAsync(user.Id, followed.Id)
            .Returns(true);

        // Act
        Result<Follower> followerResult = await _followerService.StartFollowingAsync(user, followed);

        // Assert
        followerResult.IsFailure.Should().BeTrue();
        followerResult.Error.Should().Be(FollowerErrors.AlreadyFollowing);
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnSuccess_WhenFollowerCreated()
    {
        // Arrange
        var user = User.Create(Oid, Name, Username, Email, ImageUrl);
        var followed = User.Create(Oid, Name, Username, Email, ImageUrl);

        _followerRepositoryMock
            .IsAlreadyFollowingAsync(user.Id, followed.Id)
            .Returns(false);

        // Act
        Result<Follower> followerResult = await _followerService.StartFollowingAsync(user, followed);

        // Assert
        followerResult.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnFollower_WhenFollowerCreated()
    {
        // Arrange
        var user = User.Create(Oid, Name, Username, Email, ImageUrl);
        var followed = User.Create(Oid, Name, Username, Email, ImageUrl);

        _followerRepositoryMock
            .IsAlreadyFollowingAsync(user.Id, followed.Id)
            .Returns(false);

        // Act
        Result<Follower> followerResult = await _followerService.StartFollowingAsync(user, followed);

        // Assert
        Follower follower = followerResult.Value;
        follower.UserId.Should().Be(user.Id);
        follower.FollowedId.Should().Be(followed.Id);
        follower.CreatedOnUtc.Should().Be(UtcNow);
    }

    [Fact]
    public async Task StopFollowingAsync_ShouldReturnError_WhenUnfollowingSameUser()
    {
        // Arrange
        var user = User.Create(Oid, Name, Username, Email, ImageUrl);

        // Act
        Result<Follower> followerResult = await _followerService.StopFollowingAsync(user, user);

        // Assert
        followerResult.IsFailure.Should().BeTrue();
        followerResult.Error.Should().Be(FollowerErrors.SameUser);
    }

    [Fact]
    public async Task StopFollowingAsync_ShouldReturnError_WhenNotFollowingUser()
    {
        // Arrange
        var user = User.Create(Oid, Name, Username, Email, ImageUrl);
        var followed = User.Create(Oid, Name, Username, Email, ImageUrl);

        _followerRepositoryMock
            .GetAsync(user.Id, followed.Id)
            .ReturnsNull();

        // Act
        Result<Follower> followerResult = await _followerService.StopFollowingAsync(user, followed);

        // Assert
        followerResult.IsFailure.Should().BeTrue();
        followerResult.Error.Should().Be(FollowerErrors.NotFollowing);
    }
}
