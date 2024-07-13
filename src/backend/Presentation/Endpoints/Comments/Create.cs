using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Posts.Application.Comments.Create;
using Presentation.Contracts.Comments;
using Presentation.Extensions;
using Presentation.Infrastructure;

namespace Presentation.Endpoints.Comments;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("comments", async (
            CreateCommentRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateCommentCommand(request.UserId, request.PostId, request.Content);

            var result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Comments);
    }
}
