using Application.Abstractions.Storage;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using SharedKernel;

namespace Infrastructure.Storage;

internal sealed class BlobService(BlobServiceClient client, IConfiguration configuration) 
    : IBlobService
{
    private readonly string ContainerName = configuration.GetValueOrThrow("Blob:ContainerName");

    public async Task<Result<FileResponse>> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default)
    {
        BlobContainerClient containerClient = client.GetBlobContainerClient(ContainerName);

        BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

        if (!await blobClient.ExistsAsync(cancellationToken))
        {
            return Result.Failure<FileResponse>(BlobErrors.NotFound(fileId));
        }

        Response<BlobDownloadResult> response = await blobClient.DownloadContentAsync(cancellationToken);

        return new FileResponse(response.Value.Content.ToStream(), response.Value.Details.ContentType);
    }

    public async Task<Guid> UploadAsync(
        Stream stream, 
        string contentType, 
        CancellationToken cancellationToken = default)
    {
        Guid fileId = Guid.NewGuid();

        BlobContainerClient containerClient = client.GetBlobContainerClient(ContainerName);

        BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

        await blobClient.UploadAsync(
            stream, 
            new BlobHttpHeaders { ContentType = contentType },
            cancellationToken: cancellationToken);

        return fileId;
    }

    public Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default)
    {
        BlobContainerClient containerClient = client.GetBlobContainerClient(ContainerName);

        BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

        return blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }
}
