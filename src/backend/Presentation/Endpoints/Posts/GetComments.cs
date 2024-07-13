using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Posts.Application.Comments;
using Modules.Posts.Application.Comments.GetByPostId;
using Presentation.Extensions;
using Presentation.Infrastructure;
using SharedKernel;

namespace Presentation.Endpoints.Posts;

internal sealed class GetComments : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("posts/{postId}/comments", async (
            Guid postId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetCommentsByPostIdQuery(postId);

            Result<List<CommentResponse>> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Posts);
    }
}
