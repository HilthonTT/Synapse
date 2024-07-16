using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;
using System.Data;

namespace Modules.Posts.Application.Posts.Get;

internal sealed class GetPostsQueryHandler(IDbConnectionFactory factory) 
    : IQueryHandler<GetPostsQuery, PostsCursorResponse>
{
    public async Task<Result<PostsCursorResponse>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = factory.GetOpenConnection();

        (List<PostResponse> posts, Guid? nextCursor, Guid? previousCursor) =
            await PostQueries.GetAsync(connection, request.Cursor, request.Limit);

        var response = new PostsCursorResponse(posts, previousCursor, nextCursor);

        return response;
    }
}
