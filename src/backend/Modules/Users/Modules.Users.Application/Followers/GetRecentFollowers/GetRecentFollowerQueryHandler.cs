using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;
using System.Data;

namespace Modules.Users.Application.Followers.GetRecentFollowers;

internal sealed class GetRecentFollowerQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetRecentFollowersQuery, List<FollowerResponse>>
{
    public async Task<Result<List<FollowerResponse>>> Handle(
        GetRecentFollowersQuery request, 
        CancellationToken cancellationToken)
    {
        using IDbConnection connection = factory.GetOpenConnection();

        List<FollowerResponse> followers = await FollowerQueries.GetByUserId(connection, request.UserId);

        return followers;
    }
}
