using System;
using System.IO;
using System.Linq;
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
    
    public Task<bool> DeleteUserImage(string fileUrl)
        => DeleteFile(_userImagesContainerClient, fileUrl);

    public Task<string> UploadUserImage(Stream stream, Guid imageId)
        => UploadImage(_userImagesContainerClient, stream, imageId);

    public Task<bool> DeleteShowImage(string fileUrl)
        => DeleteFile(_showImagesContainerClient, fileUrl);

    public Task<string> UploadShowImage(Stream stream, Guid imageId)
        => UploadImage(_showImagesContainerClient, stream, imageId);

    public Task<bool> DeleteBroadcastImage(string fileUrl)
        => DeleteFile(_broadcastImagesContainerClient, fileUrl);

    public Task<string> UploadBroadcastImage(Stream stream, Guid imageId)
        => UploadImage(_broadcastImagesContainerClient, stream, imageId);

    private async Task<bool> DeleteFile(BlobContainerClient client, string fileUrl)
    {
        try
        {
            var response = await client.DeleteBlobAsync(fileUrl.Split('/').Last());

            return response.Status == 202;
        }
        catch (Exception exception)
        {
            logger.LogError(exception.Message);
            return false;
        }
    }

    private static async Task<string> UploadImage(BlobContainerClient client, Stream stream, Guid imageId)
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