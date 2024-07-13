using SharedKernel;

namespace Modules.Posts.Domain.Comments;

public sealed class Comment : Entity, IAuditableEntity
{
    private Comment(Guid id, Guid postId, Guid userId, string content)
        : base(id)
    {
        PostId = postId;
        UserId = userId;
        Content = content;
    }

    private Comment()
    {
    }

    public Guid PostId { get; private set; }

    public Guid UserId { get; private set; }

    public string Content { get; private set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    public static Comment Create(Guid postId, Guid userId, string content)
    {
        var comment = new Comment(Guid.NewGuid(), postId, userId, content);

        comment.RaiseDomainEvent(new CommentCreatedDomainEvent(comment.Id));

        return comment;
    }

    public void Update(string content)
    {
        Content = content;
    }
}
