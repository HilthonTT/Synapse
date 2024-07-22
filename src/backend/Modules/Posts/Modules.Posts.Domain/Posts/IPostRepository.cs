
namespace Modules.Posts.Domain.Posts;

public interface IPostRepository
{
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Post?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Post post);

    void Remove(Post post);

    Task<List<Post>> SearchAsync(
        string? searchTerm,
        SortColumn sortColumn, 
        SortOrder sortOrder,
        int limit = 10,
        CancellationToken cancellationToken = default);
}
