﻿using Application.Abstractions.Messaging;

namespace Modules.Users.Application.Users.Update;

public sealed record UpdateUserCommand(
    Guid UserId,
    string Email,
    string Name,
    string Username,
    string ImageUrl) : ICommand<Guid>;
