using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ScreeningExchange.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace ScreeningExchange.Infrastructure.IO.Blobs;

public sealed class BlobStorage(IOptions<ApiConfig> config) : IStorage
{
    private readonly ApiConfig Config = config.Value;

    public async ValueTask<string> DownloadBase64Async(
        string filename,
        CancellationToken ct = default)
    {
        var client = await GetBlobContainer(
            Config.BlobStorage.BlobStorageConnectionString,
            Config.BlobStorage.ContainerName,
            ct);

        var blobClient = client.GetBlobClient(filename);

        if (blobClient.Exists().Value is false)
            return string.Empty;

        using MemoryStream memoryStream = new MemoryStream();

        await blobClient.DownloadToAsync(memoryStream);

        byte[] blobBytes = memoryStream.ToArray();
        string base64String = Convert.ToBase64String(blobBytes);

        return base64String;
    }

    public async ValueTask<string> UploadAsync(
        UploadParams uploadParams,
        CancellationToken ct = default)
    {
        var client = await GetBlobContainer(
            Config.BlobStorage.BlobStorageConnectionString,
            Config.BlobStorage.ContainerName,
            ct);

        var bytes = Convert.FromBase64String(uploadParams.Base64);
        using var contents = new MemoryStream(bytes);

        await client.UploadBlobAsync(uploadParams.Filename, contents, ct);

        return uploadParams.Filename;
    }

    private static async Task<BlobContainerClient> GetBlobContainer(
        string connectionString,
        string containerName,
        CancellationToken ct = default)
    {
        var blobServiceClient = new BlobServiceClient(connectionString);
        var blobClient = blobServiceClient.GetBlobContainerClient(containerName);
        await blobClient.CreateIfNotExistsAsync(cancellationToken: ct);
        return blobClient;
    }

    public async ValueTask<bool> DeleteAsync(string filename, CancellationToken ct)
    {
        var client = await GetBlobContainer(
           Config.BlobStorage.BlobStorageConnectionString,
           Config.BlobStorage.ContainerName,
           ct);

        return await client.DeleteBlobIfExistsAsync
        (
            filename,
            snapshotsOption: DeleteSnapshotsOption.IncludeSnapshots,
            cancellationToken: ct
        );
    }
}