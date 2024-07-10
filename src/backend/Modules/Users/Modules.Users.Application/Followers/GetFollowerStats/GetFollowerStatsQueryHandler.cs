using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;
using System.Data;

namespace Modules.Users.Application.Followers.GetFollowerStats;

internal sealed class GetFollowerStatsQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetFollowerStatsQuery, FollowerStatsResponse>
{
    public async Task<Result<FollowerStatsResponse>> Handle(
        GetFollowerStatsQuery request, 
        CancellationToken cancellationToken)
    {
        using IDbConnection connection = factory.GetOpenConnection();

        FollowerStatsResponse followerStats = await FollowerQueries.GetStatsAsync(connection, request.UserId);

        return followerStats;
    }
}
