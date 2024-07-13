using SharedKernel;

namespace Infrastructure.Storage;

public static class BlobErrors
{
    public static Error NotFound(Guid id) => Error.NotFound(
        "Blob.NotFound",
        $"Blob with the Id = '{id}' was not found");
}
