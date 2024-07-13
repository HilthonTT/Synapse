using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Posts.Application.Comments.Update;
using Presentation.Contracts.Comments;
using Presentation.Extensions;
using Presentation.Infrastructure;
using SharedKernel;

namespace Presentation.Endpoints.Comments;

internal sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("comments/{commentId}", async (
            Guid commentId,
            UpdateCommentRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateCommentCommand(request.UserId, commentId, request.Content);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Comments);
    }
}
