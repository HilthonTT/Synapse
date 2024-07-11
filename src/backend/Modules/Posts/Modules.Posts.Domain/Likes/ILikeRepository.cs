namespace Modules.Posts.Domain.Likes;

public interface ILikeRepository
{
    Task<Like?> GetByIdAsync(Guid postId, Guid userId, CancellationToken cancellationToken = default);

    void Insert(Like like);

    void Remove(Like like);
}
