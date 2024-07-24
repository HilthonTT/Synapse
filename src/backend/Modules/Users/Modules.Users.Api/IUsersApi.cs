namespace Modules.Users.Api;

public interface IUsersApi
{
    Task<UserApiResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default);
}
