using Application.Abstractions.Messaging;
using MediatR;
using Microsoft.AspNetCore.Http;
using Modules.Users.Application.Users.Create;
using Modules.Users.Application.Users.Update;
using Modules.Users.Domain.Users;
using SharedKernel;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace Modules.Users.Application.Users.GetFromAuth;

internal sealed class GetUserFromAuthQueryHandler(
    IHttpContextAccessor accessor,
    IUserRepository userRepository,
    ISender sender,
    ILogger<GetUserFromAuthQueryHandler> logger)
    : IQueryHandler<GetUserFromAuthQuery, UserAuthResponse>
{
    public async Task<Result<UserAuthResponse>> Handle(
        GetUserFromAuthQuery request,
        CancellationToken cancellationToken)
    {
        List<Claim>? claims = accessor.HttpContext?.User.Claims.ToList();
        if (claims is null || claims.Count == 0)
        {
            logger.LogWarning("Unauthorized access attempt due to missing claims.");
            return Result.Failure<UserAuthResponse>(UserErrors.Unauthorized);
        }

        string? objectIdentifier = GetClaimValue(claims, ClaimTypes.NameIdentifier);
        Result<ObjectIdentifier> oidResult = ObjectIdentifier.Create(objectIdentifier);
        if (oidResult.IsFailure)
        {
            logger.LogWarning("Failed to create ObjectIdentifier from claims.");
            return Result.Failure<UserAuthResponse>(UserErrors.Unauthorized);
        }

        User? user = await userRepository.GetByOidAsync(oidResult.Value, cancellationToken);

        if (user is not null)
        {
            User cleanedUser = await CleanUserAsync(claims, user, cancellationToken);

            return MapToUserAuthResponse(cleanedUser);
        }

        User newUser = await CreateUserAsync(claims, cancellationToken);

        return MapToUserAuthResponse(newUser);
    }

    private static string GetClaimValue(List<Claim> claims, string claimType)
    {
        return claims.FirstOrDefault(c => c.Type == claimType)?.Value ?? string.Empty;
    }

    private async Task<User> CleanUserAsync(
        List<Claim> claims,
        User user,
        CancellationToken cancellationToken = default)
    {
        bool isDirty = false;

        string name = GetClaimValue(claims, "name");
        string username = GetClaimValue(claims, "username");
        string email = GetClaimValue(claims, "email");
        string imageUrl = GetClaimValue(claims, "picture");

        if (name != user.Name.Value ||
            email != user.Email.Value ||
            username != user.Username.Value ||
            imageUrl != user.ImageUrl)
        {
            isDirty = true;
        }

        if (!isDirty)
        {
            return user;
        }

        var command = new UpdateUserCommand(user.Id, email, name, username, imageUrl);
        Result<Guid> result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            logger.LogError("Failed to update user: {UserId}", user.Id);
            throw new InvalidOperationException("Failed to update user");
        }

        User updatedUser = (await userRepository.GetByIdAsync(result.Value, cancellationToken))!;
        return updatedUser;
    }

    private async Task<User> CreateUserAsync(List<Claim> claims, CancellationToken cancellationToken = default)
    {
        string objectIdentifier = GetClaimValue(claims, ClaimTypes.NameIdentifier);
        string name = GetClaimValue(claims, "name");
        string username = GetClaimValue(claims, "username");
        string email = GetClaimValue(claims, "email");
        string imageUrl = GetClaimValue(claims, "picture");

        var command = new CreateUserCommand(objectIdentifier, email, name, username, imageUrl);
        Result<Guid> result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            logger.LogError("Failed to create user with OID: {ObjectIdentifier}", objectIdentifier);
            throw new InvalidOperationException("Failed to create user");
        }

        User createdUser = (await userRepository.GetByIdAsync(result.Value, cancellationToken))!;
        return createdUser;
    }

    private UserAuthResponse MapToUserAuthResponse(User user)
    {
        return new UserAuthResponse(
            user.Id,
            user.ObjectIdentifier.Value,
            user.Name.Value,
            user.Username.Value,
            user.Email.Value,
            user.ImageUrl);
    }
}
