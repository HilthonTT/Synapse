﻿using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain.Users;
using Modules.Users.Infrastructure.Database;

namespace Modules.Users.Infrastructure.Repositories;

internal sealed class UserRepository(UsersDbContext context) : IUserRepository
{
    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public Task<User?> GetByOidAsync(ObjectIdentifier objectIdentifier, CancellationToken cancellationToken = default)
    {
        return context.Users.FirstOrDefaultAsync(
            u => u.ObjectIdentifier.Value == objectIdentifier.Value, 
            cancellationToken);
    }

    public void Insert(User user)
    {
        context.Users.Add(user);
    }

    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
    {
        return !await context.Users.AnyAsync(u => u.Email.Value == email.Value, cancellationToken);
    }

    public async Task<bool> IsOidUniqueAsync(ObjectIdentifier objectIdentifier, CancellationToken cancellationToken = default)
    {
        return !await context.Users.AnyAsync(
            u => u.ObjectIdentifier.Value == objectIdentifier.Value,
            cancellationToken);
    }
}
