using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Modules.Users.Domain.Users;
using SharedKernel;
using System.Data;

namespace Modules.Users.Application.Users.GetById;

internal sealed class GetUserByIdQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = factory.GetOpenConnection();

        UserResponse? user = await UserQueries.GetByIdAsync(connection, request.UserId);

        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound(request.UserId));
        }

        return user;
    }
}
