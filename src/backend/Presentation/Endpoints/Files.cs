using Application.Abstractions.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Presentation.Extensions;
using Presentation.Infrastructure;
using SharedKernel;

namespace Presentation.Endpoints;

internal sealed class Files : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("files", async (
            IFormFile file, 
            IBlobService blobService) =>
        {
            using Stream stream = file.OpenReadStream();

            Guid fileId = await blobService.UploadAsync(stream, file.ContentType);

            return Results.Ok(fileId);
        })
        .RequireAuthorization()
        .DisableAntiforgery()
        .WithTags(Tags.Files);

        app.MapGet("files/{fileId}", async (
            Guid fileId,
            IBlobService blobService) =>
        {
            Result<FileResponse> result = await blobService.DownloadAsync(fileId);

            return result.Match(
                response => Results.File(response.Stream, response.ContentType),
                _ => CustomResults.Problem(result));
        })
        .WithTags(Tags.Files);

        app.MapDelete("files/{fileId}", async (
            Guid fileId, 
            IBlobService blobService) =>
        {
            await blobService.DeleteAsync(fileId);

            return Results.NoContent();
        })
        .RequireAuthorization()
        .WithTags(Tags.Files);
    }
}
