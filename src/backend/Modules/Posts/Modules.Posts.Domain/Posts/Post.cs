using Modules.Posts.Domain.Comments;
using Modules.Posts.Domain.Likes;
using SharedKernel;

namespace Modules.Posts.Domain.Posts;

public sealed class Post : Entity, IAuditableEntity
{
    public const int TagsMaxCount = 10;

    private Post(
        Guid id, 
        Guid userId,
        string title, 
        string imageUrl, 
        string? location,
        string? tags) 
        : base(id)
    {
        UserId = userId;
        Title = title;
        ImageUrl = imageUrl;
        Location = location;
        Tags = tags;
    }

    private Post()
    {
    }

    public Guid UserId { get; private set; }

    public string Title { get; private set; }

    public string ImageUrl { get; private set; }

    public string? Location { get; private set; }

    public string? Tags { get; set; }

    public List<Like> Likes { get; private set; } = [];

    public List<Comment> Comments { get; private set; } = [];

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    
    public void Update(
        string title,
        string imageUrl,
        string? location,
        string? tags)
    {
        Title = title;
        ImageUrl = imageUrl;
        Location = location;
        Tags = tags;
    }

    public static Post Create(Guid userId, string title, string imageUrl, string? location, string? tags)
    {
        var post = new Post(Guid.NewGuid(), userId, title, imageUrl, location, tags);

        post.RaiseDomainEvent(new PostCreatedDomainEvent(post.Id));

        return post;
    }

    public Result<Like> AddLike(Guid userId)
    {
        Like? existingLike = Likes.FirstOrDefault(l => l.UserId == userId);
        if (existingLike is not null)
        {
            return Result.Failure<Like>(LikeErrors.AlreadyLiked);
        }

        var like = Like.Create(Id, userId);

        Likes.Add(like);

        return like;
    }

    public Result<Like> RemoveLike(Guid userId)
    {
        Like? existingLike = Likes.FirstOrDefault(l => l.UserId == userId);
        if (existingLike is null)
        {
            return Result.Failure<Like>(LikeErrors.NotFound(userId));
        }

        Likes.Remove(existingLike);

        return existingLike;
    }
}
