using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Posts.Application.Likes.Remove;
using Presentation.Extensions;
using Presentation.Infrastructure;
using SharedKernel;

namespace Presentation.Endpoints.Posts;

internal sealed class RemoveLike : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("posts/{postId}/likes", async (
            Guid postId,
            [FromQuery] Guid userId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new RemoveLikeCommand(userId, postId);

            Result result = await sender.Send(command, cancellationToken);

            result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Posts);
    }
}
