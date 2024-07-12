using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;
using System.Data;

namespace Modules.Posts.Application.Likes.GetByPostId;

internal sealed class GetLikesByPostIdQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetLikesByPostIdQuery, List<LikeResponse>>
{
    public async Task<Result<List<LikeResponse>>> Handle(
        GetLikesByPostIdQuery request, 
        CancellationToken cancellationToken)
    {
        using IDbConnection connection = factory.GetOpenConnection();

        List<LikeResponse> likes = await LikeQueries.GetByPostIdAsync(connection, request.PostId);

        return likes;
    }
}
