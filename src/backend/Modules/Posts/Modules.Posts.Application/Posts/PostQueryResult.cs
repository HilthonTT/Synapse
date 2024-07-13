namespace Modules.Posts.Application.Posts;

internal sealed class PostQueryResult
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public string Tags { get; set; } = string.Empty;

    public int LikesCount { get; set; }

    public int CommentsCount { get; set; }

    public Guid UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string UserUsername { get; set; } = string.Empty;

    public string UserImageUrl { get; set; } = string.Empty;
}
