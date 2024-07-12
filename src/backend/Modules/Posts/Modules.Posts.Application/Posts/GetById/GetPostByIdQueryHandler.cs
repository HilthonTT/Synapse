using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Modules.Posts.Domain.Posts;
using SharedKernel;
using System.Data;

namespace Modules.Posts.Application.Posts.GetById;

internal sealed class GetPostByIdQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetPostByIdQuery, PostResponse>
{
    public async Task<Result<PostResponse>> Handle(
        GetPostByIdQuery request, 
        CancellationToken cancellationToken)
    {
        using IDbConnection connection = factory.GetOpenConnection();

        PostResponse? post = await PostQueries.GetByIdAsync(connection, request.PostId);
        if (post is null)
        {
            return Result.Failure<PostResponse>(PostErrors.NotFound(request.PostId));
        }

        return post;
    }
}
