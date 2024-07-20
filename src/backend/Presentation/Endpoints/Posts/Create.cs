using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Posts.Application.Posts.Create;
using Presentation.Contracts.Posts;
using Presentation.Extensions;
using Presentation.Infrastructure;
using SharedKernel;

namespace Presentation.Endpoints.Posts;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("posts", async (
            CreatePostRequest request,
            [FromHeader(Name = "X-Idempotency-Key")] Guid requestId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CreatePostCommand(
                requestId,
                request.UserId, 
                request.Title, 
                request.ImageUrl,
                request.Location,
                request.Tags);

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Posts);
    }
}
