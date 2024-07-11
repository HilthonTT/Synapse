namespace Modules.Posts.Domain.Comments;

public interface ICommentRepository
{
    Task<Comment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Comment comment);

    void Remove(Comment comment);
}
