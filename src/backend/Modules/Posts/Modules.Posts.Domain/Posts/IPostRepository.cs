namespace Modules.Posts.Domain.Posts;

public interface IPostRepository
{
    Task<Post?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Post post);

    void Remove(Post post);
}
