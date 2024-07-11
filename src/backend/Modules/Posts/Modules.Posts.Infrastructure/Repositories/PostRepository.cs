using Microsoft.EntityFrameworkCore;
using Modules.Posts.Domain.Posts;
using Modules.Posts.Infrastructure.Database;

namespace Modules.Posts.Infrastructure.Repositories;

internal sealed class PostRepository(PostsDbContext context) : IPostRepository
{
    public Task<Post?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Posts.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public void Insert(Post post)
    {
        context.Posts.Add(post);
    }

    public void Remove(Post post)
    {
        context.Posts.Remove(post);
    }
}
