using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Posts.Application.Posts.Search;
using Modules.Posts.Domain.Posts;
using Presentation.Extensions;
using Presentation.Infrastructure;
using SharedKernel;

namespace Presentation.Endpoints.Posts;

internal sealed class Search : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("posts/search", async (
            ISender sender,
            CancellationToken cancellationToken,
            [FromQuery] string? searchTerm,
            [FromQuery] SortOrder sortOrder,
            [FromQuery] SortColumn sortColumn,
            [FromQuery] int limit = 10) =>
        {
            var query = new SearchPostsQuery(searchTerm, sortOrder, sortColumn, limit);

            Result<List<SearchPostResponse>> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Posts);
    }
}
