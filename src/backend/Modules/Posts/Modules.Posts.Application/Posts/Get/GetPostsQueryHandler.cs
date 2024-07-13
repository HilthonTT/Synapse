using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;
using System.Data;

namespace Modules.Posts.Application.Posts.Get;

internal sealed class GetPostsQueryHandler(IDbConnectionFactory factory) 
    : IQueryHandler<GetPostsQuery, List<PostResponse>>
{
    public async Task<Result<List<PostResponse>>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = factory.GetOpenConnection();

        List<PostResponse> posts = await PostQueries.GetAsync(connection, request.Cursor, request.Limit);

        return posts;
    }
}
