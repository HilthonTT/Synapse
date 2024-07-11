using SharedKernel;

namespace Modules.Posts.Domain.Likes;

public sealed class Like : Entity
{
    private Like(Guid id, Guid postId, Guid userId)
        : base(id)
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
        var like = new Like(Guid.NewGuid(), postId, userId);

        like.RaiseDomainEvent(new LikeCreatedDomainEvent(like.Id));

        return like;
    }
}
