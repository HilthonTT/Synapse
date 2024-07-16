using FluentAssertions;
using Modules.Posts.Domain.Comments;

namespace Domain.UnitTests.Comments;

public sealed class CommentTests
{
    [Fact]
    public void Create_Should_RaiseDomainEvent()
    {
        // Arrange
        Guid postId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();

        const string content = "content";

        // Act
        var comment = Comment.Create(postId, userId, content);

        // Assert
        comment.DomainEvents
            .Should()
            .ContainSingle()
            .Which
            .Should()
            .BeOfType<CommentCreatedDomainEvent>();
    }
}
