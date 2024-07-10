using SharedKernel;

namespace Modules.Users.Domain.Followers;

public static class FollowerErrors
{
    public static readonly Error SameUser = Error.Problem(
       "Followers.SameUser",
       "Can't follow yourself or unfollow yourself");

    public static readonly Error AlreadyFollowing = Error.Conflict(
        "Followers.AlreadyFollowing",
        "Already following");

    public static readonly Error NotFollowing = Error.Conflict(
       "Followers.NotFollowing",
       "Can't unfollow someone you don't follow");
}
