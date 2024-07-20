using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Posts.Application.Posts.Remove;
using Presentation.Extensions;
using Presentation.Infrastructure;
using SharedKernel;

namespace Presentation.Endpoints.Posts;

internal sealed class Remove : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("posts/{postId}", async (
            [FromQuery] Guid userId,
            Guid postId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new RemovePostCommand(userId, postId);

            Result result = await sender.Send(command, cancellationToken);

            result.Match(Results.NoContent, CustomResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.Posts);
    }
}
