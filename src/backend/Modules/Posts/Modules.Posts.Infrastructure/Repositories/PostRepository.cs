using Microsoft.EntityFrameworkCore;
using Modules.Posts.Domain.Posts;
using Modules.Posts.Infrastructure.Database;
using System.Linq.Expressions;

namespace Modules.Posts.Infrastructure.Repositories;

internal sealed class PostRepository(PostsDbContext context) : IPostRepository
{
    public async Task<List<Post>> SearchAsync(
         string? searchTerm,
         SortColumn sortColumn,
         SortOrder sortOrder,
         int limit = 10,
         CancellationToken cancellationToken = default)
    {
        IQueryable<Post> postsQuery = context.Posts
            .Include(p => p.Likes)
            .Include(p => p.Comments);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            postsQuery = postsQuery.Where(
                p => p.Title.Contains(searchTerm) ||
                (!string.IsNullOrWhiteSpace(p.Tags) && p.Tags.Contains(searchTerm)) ||
                (!string.IsNullOrWhiteSpace(p.Location) && p.Location.Contains(searchTerm)));
        }

        Expression<Func<Post, object>> keySelector = GetSortProperty(sortColumn);

        postsQuery = sortOrder == SortOrder.Descending
            ? postsQuery.OrderByDescending(keySelector)
            : postsQuery.OrderBy(keySelector);

        postsQuery = postsQuery.Take(limit);

        List<Post> posts = await postsQuery.ToListAsync(cancellationToken);

        return posts;
    }

    public Task<Post?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Posts
            .Include(p => p.Likes)
            .Include(p => p.Comments)
            .AsSplitQuery()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Posts.AnyAsync(p => p.Id == id, cancellationToken);
    }

    public void Insert(Post post)
    {
        context.Posts.Add(post);
    }

    public void Remove(Post post)
    {
        context.Posts.Remove(post);
    }

    private static Expression<Func<Post, object>> GetSortProperty(SortColumn sortColumn)
    {
        Expression<Func<Post, object>> keySelector = sortColumn switch
        {
            SortColumn.Likes => post => post.Likes.Count,
            SortColumn.Comments => post => post.Comments.Count,
            SortColumn.CreatedOnUtc => post => post.CreatedOnUtc,
            SortColumn.Title => post => post.Title,
            _ => post => post.Id,
        };

        return keySelector;
    }
}
