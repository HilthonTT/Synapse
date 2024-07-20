using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Posts.Application.Posts.Update;
using Presentation.Contracts.Posts;
using Presentation.Extensions;
using Presentation.Infrastructure;
using SharedKernel;

namespace Presentation.Endpoints.Posts;

internal sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("posts/{postId}", async (
            Guid postId,
            UpdatePostRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdatePostCommand(
                postId, 
                request.UserId, 
                request.Title, 
                request.ImageUrl, 
                request.Location, 
                request.Tags);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.Posts);
    }
}
