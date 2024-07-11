using Microsoft.EntityFrameworkCore;
using Modules.Posts.Domain.Likes;
using Modules.Posts.Infrastructure.Database;

namespace Modules.Posts.Infrastructure.Repositories;

internal sealed class LikeRepository(PostsDbContext context) : ILikeRepository
{
    public Task<Like?> GetByIdAsync(Guid postId, Guid userId, CancellationToken cancellationToken = default)
    {
        return context.Likes.FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId, cancellationToken);
    }

    public void Insert(Like like)
    {
        context.Likes.Add(like);
    }

    public void Remove(Like like)
    {
        context.Remove(like);
    }
}
