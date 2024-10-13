using System;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tlis.Cms.Domain.Constants;
using Tlis.Cms.Infrastructure.Configurations;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Infrastructure.Services;

internal sealed class CloudStorageService(
    ILogger<CloudStorageService> logger,
    IOptions<CloudStorageConfiguration> cloudeStorageConfiguration) : ICloudeStorageService
{
    private readonly BlobContainerClient _userImagesContainerClient = new(
        cloudeStorageConfiguration.Value.Authentication.ConnectionString,
        cloudeStorageConfiguration.Value.Cdn.Folders.UserImages);

    private readonly BlobContainerClient _showImagesContainerClient = new(
        cloudeStorageConfiguration.Value.Authentication.ConnectionString,
        cloudeStorageConfiguration.Value.Cdn.Folders.ShowImages);

    private readonly BlobContainerClient _broadcastImagesContainerClient = new(
        cloudeStorageConfiguration.Value.Authentication.ConnectionString,
        cloudeStorageConfiguration.Value.Cdn.Folders.BroadcastImages);
    
    public Task<bool> DeleteUserImageAsync(string fileName)
        => DeleteFileAsync(_userImagesContainerClient, fileName);

    public Task<string> UploadUserImageAsync(Stream stream, Guid imageId)
        => UploadImageAsync(_userImagesContainerClient, stream, imageId);

    public Task<bool> DeleteShowImageAsync(string fileName)
        => DeleteFileAsync(_showImagesContainerClient, fileName);

    public Task<string> UploadShowImageAsync(Stream stream, Guid imageId)
        => UploadImageAsync(_showImagesContainerClient, stream, imageId);

    public Task<bool> DeleteBroadcastImageAsync(string fileName)
        => DeleteFileAsync(_broadcastImagesContainerClient, fileName);

    public Task<string> UploadBroadcastImageAsync(Stream stream, Guid imageId)
        => UploadImageAsync(_broadcastImagesContainerClient, stream, imageId);

    private async Task<bool> DeleteFileAsync(BlobContainerClient client, string fileName)
    {
        try
        {
            var response = await client.DeleteBlobAsync(fileName);

            return response.Status == 202;
        }
        catch (Exception exception)
        {
            logger.LogError(exception.Message);
            return false;
        }
    }

    private static async Task<string> UploadImageAsync(BlobContainerClient client, Stream stream, Guid imageId)
    {
        var storageFileName = GetStorageFileName(imageId, ImageFormat.WEBP);
        var blob = client.GetBlobClient(storageFileName);
        stream.Position = 0;

        await blob.UploadAsync(stream, new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders
            {
                ContentType = MediaTypeNames.Image.Webp
            }
        });

        return storageFileName;
    }

    private static string GetStorageFileName(Guid guid, ImageFormat format)
    {
        var postfix = (Enum.GetName(format)?.ToLower()) ?? throw new NullReferenceException();

        return $"{guid}.{postfix}";
    }
}