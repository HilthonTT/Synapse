using MediatR;
using Modules.Users.Application.Abstractions.Data;
using Modules.Users.Application.Users.Create;
using Modules.Users.Domain.Users;
using NSubstitute;
using SharedKernel;

namespace Application.UnitTests.Users;

public sealed class CreateUserCommandTests
{
    private static readonly CreateUserCommand Command = new("test-oid", "test@test.com", "Name", "FullName", "ImageUrl");

    private readonly CreateUserCommandHandler _handler;
    private readonly IUserRepository _userRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IPublisher _publisherMock;

    public CreateUserCommandTests()
    {
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _publisherMock = Substitute.For<IPublisher>();

        _handler = new CreateUserCommandHandler(
            _userRepositoryMock,
            _publisherMock,
            _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenEmailIsInvalid()
    {
        // Arrange
        CreateUserCommand invalidCommand = Command with { Email = "not_an_email" };

        // Act
        Result<Guid> result = await _handler.Handle(invalidCommand, default);

        // Assert
        result.Error.Should().Be(EmailErrors.InvalidFormat);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenEmailIsNotUnique()
    {
        // Arrange
        _userRepositoryMock.IsEmailUniqueAsync(Arg.Is<Email>(e => e.Value == Command.Email))
            .Returns(false);

        _userRepositoryMock.IsOidUniqueAsync(Arg.Is<ObjectIdentifier>(e => e.Value == Command.ObjectIdentifier))
            .Returns(true);

        // Act
        Result<Guid> result = await _handler.Handle(Command, default);

        // Assert
        result.Error.Should().Be(UserErrors.EmailNotUnique);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenOidIsNotUnique()
    {
        // Arrange
        _userRepositoryMock.IsEmailUniqueAsync(Arg.Is<Email>(e => e.Value == Command.Email))
            .Returns(true);

        _userRepositoryMock.IsOidUniqueAsync(Arg.Is<ObjectIdentifier>(e => e.Value == Command.ObjectIdentifier))
            .Returns(false);

        // Act
        Result<Guid> result = await _handler.Handle(Command, default);

        // Assert
        result.Error.Should().Be(UserErrors.OidNotUnique);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenCreateSucceeds()
    {
        // Arrange
        _userRepositoryMock.IsEmailUniqueAsync(Arg.Is<Email>(e => e.Value == Command.Email))
            .Returns(true);

        _userRepositoryMock.IsOidUniqueAsync(Arg.Is<ObjectIdentifier>(e => e.Value == Command.ObjectIdentifier))
            .Returns(true);

        // Act
        Result<Guid> result = await _handler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallRepository_WhenCreateSucceeds()
    {
        // Arrange
        _userRepositoryMock.IsEmailUniqueAsync(Arg.Is<Email>(e => e.Value == Command.Email))
             .Returns(true);

        _userRepositoryMock.IsOidUniqueAsync(Arg.Is<ObjectIdentifier>(e => e.Value == Command.ObjectIdentifier))
            .Returns(true);

        // Act
        Result<Guid> result = await _handler.Handle(Command, default);

        // Assert
        _userRepositoryMock.Received(1).Insert(Arg.Is<User>(u => u.Id == result.Value));
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenCreatedSucceeds()
    {
        // Arrange
        _userRepositoryMock.IsEmailUniqueAsync(Arg.Is<Email>(e => e.Value == Command.Email))
            .Returns(true);

        _userRepositoryMock.IsOidUniqueAsync(Arg.Is<ObjectIdentifier>(e => e.Value == Command.ObjectIdentifier))
            .Returns(true);

        // Act
        await _handler.Handle(Command, default);

        // Assert
        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
