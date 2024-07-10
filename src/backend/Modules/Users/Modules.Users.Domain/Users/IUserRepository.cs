namespace Modules.Users.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);

    Task<bool> IsOidUniqueAsync(ObjectIdentifier objectIdentifier, CancellationToken cancellationToken = default);

    void Insert(User user);
}
