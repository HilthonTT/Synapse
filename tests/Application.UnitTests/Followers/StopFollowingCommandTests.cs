using MediatR;
using Modules.Users.Application.Abstractions.Data;
using Modules.Users.Application.Followers.StopFollowing;
using Modules.Users.Domain.Followers;
using Modules.Users.Domain.Users;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using SharedKernel;

namespace Application.UnitTests.Followers;

public sealed class StopFollowingCommandTests
{
    private static readonly User User = User.CreateTest();
    private static readonly StopFollowingCommand Command = new(Guid.NewGuid(), Guid.NewGuid());

    private readonly StopFollowingCommandHandler _handler;
    private readonly IUserRepository _userRepositoryMock;
    private readonly IFollowerService _followerServiceMock;
    private readonly IFollowerRepository _followerRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IPublisher _publisherMock;

    public StopFollowingCommandTests()
    {
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _followerServiceMock = Substitute.For<IFollowerService>();
        _followerRepositoryMock = Substitute.For<IFollowerRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _publisherMock = Substitute.For<IPublisher>();

        _handler = new StopFollowingCommandHandler(
            _userRepositoryMock,
            _followerServiceMock,
            _followerRepositoryMock,
            _publisherMock,
            _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenUserNotFound()
    {
        // Arrange
        _userRepositoryMock.GetByIdAsync(Command.UserId)
            .ReturnsNull();

        // Act
        Result result = await _handler.Handle(Command, default);

        // Assert
        result.Error.Should().Be(UserErrors.NotFound(Command.UserId));
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenFollowedNotFound()
    {
        // Arrange
        _userRepositoryMock.GetByIdAsync(Command.UserId)
            .Returns(User);

        _userRepositoryMock.GetByIdAsync(Command.FollowedId)
            .ReturnsNull();

        // Act
        Result result = await _handler.Handle(Command, default);

        // Assert
        result.Error.Should().Be(UserErrors.NotFound(Command.FollowedId));
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenSameUser()
    {
        // Arrange
        _userRepositoryMock.GetByIdAsync(Command.UserId)
            .Returns(User);

        _userRepositoryMock.GetByIdAsync(Command.FollowedId)
            .Returns(User);


        _followerServiceMock.StopFollowingAsync(User, User, default)
            .Returns(Result.Failure<Follower>(FollowerErrors.SameUser));

        // Act
        Result result = await _handler.Handle(Command, default);

        // Assert
        result.Error.Should().Be(FollowerErrors.SameUser);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenNotFollowing()
    {
        // Arrange
        _userRepositoryMock.GetByIdAsync(Command.UserId)
            .Returns(User);

        _userRepositoryMock.GetByIdAsync(Command.FollowedId)
            .Returns(User);

        _followerServiceMock.StopFollowingAsync(User, User, default)
            .Returns(Result.Failure<Follower>(FollowerErrors.NotFollowing));

        // Act
        Result result = await _handler.Handle(Command, default);

        // Assert
        result.Error.Should().Be(FollowerErrors.NotFollowing);
    }

    [Fact]
    public async Task Handle_Should_CallInsertOnRepository_WhenStopFollowingDoesNotFail()
    {
        // Arrange
        _userRepositoryMock.GetByIdAsync(Command.UserId)
           .Returns(User);

        _userRepositoryMock.GetByIdAsync(Command.FollowedId)
            .Returns(User);

        DateTime utcNow = DateTime.UtcNow;
        _followerServiceMock.StopFollowingAsync(User, User, default)
            .Returns(Follower.Create(Command.UserId, Command.FollowedId, utcNow));

        // Act
        await _handler.Handle(Command, default);

        // Assert
        _followerRepositoryMock.Received(1)
            .Remove(Arg.Is<Follower>(f => f.UserId == Command.UserId &&
                                          f.FollowedId == Command.FollowedId &&
                                          f.CreatedOnUtc == utcNow));
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenStopFollowingDoesNotFail()
    {
        // Arrange
        _userRepositoryMock.GetByIdAsync(Command.UserId)
           .Returns(User);

        _userRepositoryMock.GetByIdAsync(Command.FollowedId)
            .Returns(User);

        _followerServiceMock.StopFollowingAsync(User, User, default)
            .Returns(Follower.Create(Command.UserId, Command.FollowedId, DateTime.UtcNow));

        // Act
        Result result = await _handler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenStopFollowingDoesNotFail()
    {
        // Arrange
        _userRepositoryMock.GetByIdAsync(Command.UserId)
            .Returns(User);

        _userRepositoryMock.GetByIdAsync(Command.FollowedId)
            .Returns(User);

        _followerServiceMock.StopFollowingAsync(User, User, default)
            .Returns(Follower.Create(Command.UserId, Command.FollowedId, DateTime.UtcNow));

        // Act
        await _handler.Handle(Command, default);

        // Assert
        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_Should_CallPublisher_WhenStoppedFollowing_DoesNotFail()
    {
        // Assert
        _userRepositoryMock.GetByIdAsync(Command.UserId)
           .Returns(User);

        _userRepositoryMock.GetByIdAsync(Command.FollowedId)
            .Returns(User);

        _followerServiceMock.StopFollowingAsync(User, User, default)
            .Returns(Follower.Create(Command.UserId, Command.FollowedId, DateTime.UtcNow));

        // Act
        await _handler.Handle(Command, default);

        // Assert
        await _publisherMock.Received(1).Publish(Arg.Any<FollowingStoppedEvent>(), Arg.Any<CancellationToken>());
    }
}
