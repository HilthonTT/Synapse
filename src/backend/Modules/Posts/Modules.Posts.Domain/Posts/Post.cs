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

    public List<string> Tags => [.. _tags];

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
        if (Tags.Count > TagsMaxCount)
        {
            return Result.Failure(PostErrors.TagsMaxCount);
        }

        Tags.Add(tag);

        return Result.Success();
    }

    public Result RemoveTag(string tag)
    {
        string? existingTag = Tags.FirstOrDefault(t => t == tag);

        if (string.IsNullOrWhiteSpace(existingTag))
        {
            return Result.Failure(TagErrors.NotFound(tag));
        }

        Tags.Remove(existingTag);

        return Result.Success();
    }
}
