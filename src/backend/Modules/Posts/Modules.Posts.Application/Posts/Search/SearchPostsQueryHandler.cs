using Application.Abstractions.Messaging;
using Modules.Posts.Domain.Posts;
using SharedKernel;
using System.Data;

namespace Modules.Posts.Application.Posts.Search;

internal sealed class SearchPostsQueryHandler(IPostRepository postRepository) 
    : IQueryHandler<SearchPostsQuery, List<SearchPostResponse>>
{
    public async Task<Result<List<SearchPostResponse>>> Handle(SearchPostsQuery request, CancellationToken cancellationToken)
    {
        List<Post> posts = await postRepository.SearchAsync(
            request.SearchTerm,
            request.SortColumn,
            request.SortOrder,
            request.Limit,
            cancellationToken);

        List<SearchPostResponse> response = posts
            .Select(p => new SearchPostResponse(
                p.Id,
                p.UserId,
                p.Title,
                p.ImageUrl,
                p.Tags,
                p.Location,
                p.Likes.Count,
                p.Comments.Count,
                p.CreatedOnUtc,
                p.ModifiedOnUtc))
            .ToList();

        return response;
    }
}
