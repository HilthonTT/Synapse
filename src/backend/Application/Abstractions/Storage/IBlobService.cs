using SharedKernel;

namespace Application.Abstractions.Storage;

public interface IBlobService
{
    Task<Result<FileResponse>> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default);

    Task<Guid> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default);
}
