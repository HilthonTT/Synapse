using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;
using System.Data;

namespace Modules.Posts.Application.Posts.GetByUserId;

internal sealed class GetPostsByUserIdQueryHandler(IDbConnectionFactory factory) 
    : IQueryHandler<GetPostsByUserIdQuery, List<PostResponse>>
{
    public async Task<Result<List<PostResponse>>> Handle(
        GetPostsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        using IDbConnection connection = factory.GetOpenConnection();

        List<PostResponse> posts = await PostQueries.GetByUserIdAsync(connection, request.UserId);

        return posts;
    }
}
