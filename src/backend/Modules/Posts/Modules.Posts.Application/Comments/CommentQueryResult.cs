namespace Modules.Posts.Application.Comments;

internal sealed class CommentQueryResult
{
    public Guid Id { get; set; }

    public Guid PostId { get; set; }

    public Guid UserId { get; set; }

    public string Content { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public DateTime CreatedOnUtc { get; set; }

    public DateTime ModifiedOnUtc { get; set; }
}
