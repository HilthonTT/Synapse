using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;
using System.Data;

namespace Modules.Users.Application.Users.Get;

internal sealed class GetUsersQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetUsersQuery, List<UserResponse>>
{
    public async Task<Result<List<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = factory.GetOpenConnection();

        List<UserResponse> users = await UserQueries.GetAsync(connection, request.Limit);

        return users;
    }
}
