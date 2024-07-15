namespace Modules.Posts.Application.Posts.Get;

public sealed record PostsCursorResponse(
    List<PostResponse> Posts, 
    Guid? NextCursor);
