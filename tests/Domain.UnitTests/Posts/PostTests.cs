using FluentAssertions;
using Modules.Posts.Domain.Posts;

namespace Domain.UnitTests.Posts;

public sealed class PostTests
{
    [Fact]
    public void Create_Should_RaiseDomainEvent()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        const string title = "title";
        const string imageUrl = "imageurl";
        const string location = "location";
        const string tags = "tags";

        // Act
        var post = Post.Create(userId, title, imageUrl, location, tags);

        // Assert
        post.DomainEvents
            .Should()
            .ContainSingle()
            .Which
            .Should()
            .BeOfType<PostCreatedDomainEvent>();
    }
}
