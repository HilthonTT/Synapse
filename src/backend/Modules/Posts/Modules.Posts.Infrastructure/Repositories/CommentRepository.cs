using Microsoft.EntityFrameworkCore;
using Modules.Posts.Domain.Comments;
using Modules.Posts.Infrastructure.Database;

namespace Modules.Posts.Infrastructure.Repositories;

internal sealed class CommentRepository(PostsDbContext context) : ICommentRepository
{
    public Task<Comment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Comments.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public void Insert(Comment comment)
    {
        context.Comments.Add(comment);
    }

    public void Remove(Comment comment)
    {
        context.Comments.Remove(comment);
    }
}
