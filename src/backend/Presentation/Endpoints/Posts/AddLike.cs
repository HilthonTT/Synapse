using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Posts.Application.Likes.Create;
using Presentation.Contracts.Likes;
using Presentation.Extensions;
using Presentation.Infrastructure;
using SharedKernel;

namespace Presentation.Endpoints.Posts;

internal sealed class AddLike : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("posts/{postId}/likes", async (
             CreateLikeRequest request,
             Guid postId,
             ISender sender,
             CancellationToken cancellationToken) =>
        {
            var command = new CreateLikeCommand(request.UserId, postId);

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
         .RequireAuthorization()
         .WithTags(Tags.Posts);
    }
}
