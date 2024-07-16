using Application.Abstractions.Messaging;
using MediatR;
using Modules.Users.Application.Abstractions.Data;
using Modules.Users.Domain.Users;
using SharedKernel;

namespace Modules.Users.Application.Users.Create;

internal sealed class CreateUserCommandHandler(
    IUserRepository userRepository, 
    IPublisher publisher,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Result<Email> emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure)
        {
            return Result.Failure<Guid>(emailResult.Error);
        }

        if (!await userRepository.IsEmailUniqueAsync(emailResult.Value, cancellationToken))
        {
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);
        }

        Result<ObjectIdentifier> oidResult = ObjectIdentifier.Create(request.ObjectIdentifier);
        if (oidResult.IsFailure)
        {
            return Result.Failure<Guid>(oidResult.Error);
        }

        if (!await userRepository.IsOidUniqueAsync(oidResult.Value, cancellationToken))
        {
            return Result.Failure<Guid>(UserErrors.OidNotUnique);
        }

        Result<Username> usernameResult = Username.Create(request.Username);
        if (usernameResult.IsFailure)
        {
            return Result.Failure<Guid>(usernameResult.Error);
        }

        Result<Name> nameResult = Name.Create(request.Name);
        if (nameResult.IsFailure)
        {
            return Result.Failure<Guid>(nameResult.Error);
        }

        Email email = emailResult.Value;
        ObjectIdentifier oid = oidResult.Value;
        Username username = usernameResult.Value;
        Name name = nameResult.Value;

        var user = User.Create(oid, name, username, email, request.ImageUrl);

        userRepository.Insert(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await publisher.Publish(new UserCreatedEvent(user.Id), cancellationToken);

        return user.Id;
    }
}
