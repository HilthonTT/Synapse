﻿using Modules.Users.Domain.Users;
using SharedKernel;

namespace Modules.Users.Domain.Followers;

public interface IFollowerService
{
    Task<Result<Follower>> StartFollowingAsync(
        User user,
        User followed,
        CancellationToken cancellationToken = default);

    Task<Result<Follower>> StopFollowingAsync(
        User user, 
        User followed, 
        CancellationToken cancellationToken = default);
}
