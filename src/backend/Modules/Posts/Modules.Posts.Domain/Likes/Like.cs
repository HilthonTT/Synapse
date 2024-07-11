using SharedKernel;

namespace Modules.Posts.Domain.Likes;

public sealed class Like : Entity
{
    private Like(Guid postId, Guid userId)
    {
        PostId = postId;
        UserId = userId;
    }

    private Like()
    {
    }

    public Guid PostId { get; private set; }

    public Guid UserId { get; private set; }

    public static Like Create(Guid postId, Guid userId)
    {
        var like = new Like(postId, userId);

        like.RaiseDomainEvent(new LikeCreatedDomainEvent(postId, userId));

        return like;
    }
}
