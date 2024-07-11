using Dapper;
using Modules.Posts.Application.Comments;
using Modules.Posts.Application.Likes;
using Modules.Users.Api;
using System.Data;

namespace Modules.Posts.Application.Posts;

public static class PostQueries
{
    public static async Task<List<PostResponse>> GetAsync(IDbConnection connection)
    {
        const string sql =
            """
            SELECT
                p.id AS Id,
                p.title AS Title,
                p.image_url AS ImageUrl,
                p.tags AS Tags,
                c.id AS CommentId,
                c.post_id AS PostId,
                c.user_id AS UserId,
                c.created_on_utc AS CreatedOnUtc,
                c.modified_on_utc AS ModifiedOnUtc,
                u.id AS UserId,
                u.name AS UserName,
                u.username AS UserUsername,
                u.image_url AS UserImageUrl,
                l.post_id AS PostId,
                l.user_id AS LikeUserId
            FROM posts.posts p
            LEFT JOIN posts.comments c ON c.post_id = p.id
            LEFT JOIN users.users u ON u.id = c.user_id
            LEFT JOIN posts.likes l ON l.post_id = p.id
            """;

        var postDictionary = new Dictionary<Guid, PostResponse>();

        IEnumerable<PostResponse> result = 
            await connection.QueryAsync<PostResponse, CommentResponse, UserResponse, LikeResponse, PostResponse>(
            sql,
            (post, comment, user, like) =>
            {
                if (!postDictionary.TryGetValue(post.Id, out var currentPost))
                {
                    currentPost = new PostResponse(post.Id, post.Title, post.ImageUrl, post.Tags, [], []);
                    postDictionary.Add(post.Id, currentPost);
                }

                if (comment is not null)
                {
                    comment = comment with { User = user };
                    currentPost.Comments.Add(comment);
                }

                if (like is not null)
                {
                    currentPost.Likes.Add(like);
                }

                return currentPost;
            },
            splitOn: "CommentId,UserId,PostId");

        return [.. postDictionary.Values];
    }

    public static async Task<PostResponse?> GetByIdAsync(
        IDbConnection connection,
        Guid postId)
    {
        const string sql =
            """
            SELECT
                p.id AS Id,
                p.title AS Title,
                p.image_url AS ImageUrl,
                p.tags AS Tags,
                c.id AS CommentId,
                c.post_id AS PostId,
                c.user_id AS UserId,
                c.created_on_utc AS CreatedOnUtc,
                c.modified_on_utc AS ModifiedOnUtc,
                u.id AS UserId,
                u.name AS UserName,
                u.username AS UserUsername,
                u.image_url AS UserImageUrl,
                l.post_id AS PostId,
                l.user_id AS LikeUserId
            FROM posts.posts p
            LEFT JOIN posts.comments c ON c.post_id = p.id
            LEFT JOIN users.users u ON u.id = c.user_id
            LEFT JOIN posts.likes l ON l.post_id = p.id
            WHERE p.id = @PostId
            """;

        var postDictionary = new Dictionary<Guid, PostResponse>();

        var result = await connection.QueryAsync<PostResponse, CommentResponse, UserResponse, LikeResponse, PostResponse>(
            sql,
            (post, comment, user, like) =>
            {
                if (!postDictionary.TryGetValue(post.Id, out var currentPost))
                {
                    currentPost = new PostResponse(post.Id, post.Title, post.ImageUrl, post.Tags, [], []);
                    postDictionary.Add(post.Id, currentPost);
                }

                if (comment is not null)
                {
                    comment = comment with { User = user };
                    currentPost.Comments.Add(comment);
                }

                if (like is not null)
                {
                    currentPost.Likes.Add(like);
                }

                return currentPost;
            },
            new { PostId = postId },
            splitOn: "CommentId,UserId,PostId");

        if (!postDictionary.TryGetValue(postId, out PostResponse? post))
        {
            return null;
        }

        return post;
    }
}
