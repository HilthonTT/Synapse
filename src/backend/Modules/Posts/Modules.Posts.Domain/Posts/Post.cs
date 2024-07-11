using Modules.Posts.Domain.Comments;
using Modules.Posts.Domain.Likes;
using SharedKernel;

namespace Modules.Posts.Domain.Posts;

public sealed class Post : Entity, IAuditableEntity
{
    public const int TagsMaxCount = 10;

    private readonly List<string> _tags = [];

    private Post(
        Guid id, 
        Guid userId,
        string title, 
        string imageUrl, 
        string? location) 
        : base(id)
    {
        UserId = userId;
        Title = title;
        ImageUrl = imageUrl;
        Location = location;
    }

    private Post()
    {
    }

    public Guid UserId { get; private set; }

    public string Title { get; private set; }

    public string ImageUrl { get; private set; }

    public string? Location { get; private set; }

    public IReadOnlyList<string> Tags => [.. _tags];

    public List<Like> Likes { get; private set; } = [];

    public List<Comment> Comments { get; private set; } = [];

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    public static Post Create(Guid userId, string title, string imageUrl, string? location)
    {
        var post = new Post(Guid.NewGuid(), userId, title, imageUrl, location);

        post.RaiseDomainEvent(new PostCreatedDomainEvent(post.Id));

        return post;
    }

    public Result AddTag(string tag)
    {
        if (_tags.Count > TagsMaxCount)
        {
            return Result.Failure(PostErrors.TagsMaxCount);
        }

        _tags.Add(tag);

        return Result.Success();
    }

    public Result RemoveTag(string tag)
    {
        string? existingTag = _tags.FirstOrDefault(t => t == tag);

        if (string.IsNullOrWhiteSpace(existingTag))
        {
            return Result.Failure(TagErrors.NotFound(tag));
        }

        _tags.Remove(existingTag);

        return Result.Success();
    }

    public Result AddLike(Guid userId)
    {
        Like? existingLike = Likes.FirstOrDefault(l => l.UserId == userId);
        if (existingLike is not null)
        {
            return Result.Failure(LikeErrors.AlreadyLiked);
        }

        var like = Like.Create(Id, userId);

        Likes.Add(like);

        return Result.Success();
    }

    public Result RemoveLike(Guid userId)
    {
        Like? existingLike = Likes.FirstOrDefault(l => l.UserId == userId);
        if (existingLike is null)
        {
            return Result.Failure(LikeErrors.NotFound(userId));
        }

        Likes.Remove(existingLike);

        return Result.Success();
    }


    public Result AddComment(Guid userId, string content)
    {
        var comment = Comment.Create(Id, userId, content);

        Comments.Add(comment);

        return Result.Success();
    }

    public Result RemoveComment(Guid commentId)
    {
        Comment? existingComment = Comments.FirstOrDefault(l => l.Id == commentId);
        if (existingComment is null)
        {
            return Result.Failure(CommentErrors.NotFound(commentId));
        }

        Comments.Remove(existingComment);

        return Result.Success();
    }
}
