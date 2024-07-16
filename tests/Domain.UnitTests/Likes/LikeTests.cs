using FluentAssertions;
using Modules.Posts.Domain.Likes;

namespace Domain.UnitTests.Likes;

public sealed class LikeTests
{
    [Fact]
    public void Create_Should_RaiseDomainEvent()
    {
        // Arrange
        Guid postId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();

        // Act
        var like = Like.Create(postId, userId);

        // Assert
        like.DomainEvents
            .Should()
            .ContainSingle()
            .Which
            .Should().BeOfType<LikeCreatedDomainEvent>();
    }
}
