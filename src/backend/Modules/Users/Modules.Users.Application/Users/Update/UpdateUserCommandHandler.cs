using Application.Abstractions.Messaging;
using MediatR;
using Modules.Users.Application.Abstractions.Data;
using Modules.Users.Domain.Users;
using SharedKernel;

namespace Modules.Users.Application.Users.Update;

internal sealed class UpdateUserCommandHandler(
    IUserRepository userRepository, 
    IPublisher publisher,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(request.UserId));
        }

        Result<Email> emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure)
        {
            return Result.Failure<Guid>(emailResult.Error);
        }

        if (!await userRepository.IsEmailUniqueAsync(emailResult.Value, cancellationToken))
        {
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);
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
        Username username = usernameResult.Value;
        Name name = nameResult.Value;

        user.Update(name, username, email, request.ImageUrl);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await publisher.Publish(new UserUpdatedEvent(user.Id), cancellationToken);

        return user.Id;
    }
}
