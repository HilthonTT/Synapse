using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;
using System.Data;

namespace Modules.Posts.Application.Comments.GetByPostId;

internal sealed class GetCommentsByPostIdQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetCommentsByPostIdQuery, List<CommentResponse>>
{
    public async Task<Result<List<CommentResponse>>> Handle(
        GetCommentsByPostIdQuery request, 
        CancellationToken cancellationToken)
    {
        using IDbConnection connection = factory.GetOpenConnection();

        List<CommentResponse> comments = await CommentQueries.GetByPostIdAsync(connection, request.PostId);

        return comments;
    }
}
