using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tlis.Cms.Domain.Constants;
using Tlis.Cms.Infrastructure.Configurations;
using Tlis.Cms.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.Infrastructure.Services;

internal sealed class StorageService(
    IConfiguration configuration,
    ILogger<StorageService> logger,
    IOptions<ServiceUrlsConfiguration> serviceUrlsConfiguration) : IStorageService
{
    private const string ImagesContainer = "images";
    
    private readonly string _storageAccountUrl = serviceUrlsConfiguration.Value.StorageAccount;

    private readonly BlobContainerClient _imagesContainerClient = new(
        configuration.GetConnectionString("StorageAccount"), ImagesContainer);
    
    public Task<bool> DeleteImage(string fileUrl)
        => DeleteFile(_imagesContainerClient, fileUrl);

    public Task<string> UploadImage(Stream stream, Guid imageId)
        => UploadImage(_imagesContainerClient, stream, ImagesContainer, imageId);

    public Task UpdateImage(Stream stream, Guid imageId)
        => UpdateImage(_imagesContainerClient, stream, ImagesContainer, imageId);

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

    private async Task UpdateImage(
        BlobContainerClient client,
        Stream stream,
        string containerName,
        Guid imageId)
    {
        await UploadImage(client, stream, containerName, imageId);
    }

    private async Task<string> UploadImage(
        BlobContainerClient client,
        Stream stream,
        string containerName,
        Guid imageId)
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

        return Path.Combine(_storageAccountUrl, containerName, storageFileName);
    }

    private static string GetStorageFileName(Guid guid, ImageFormat format)
    {
        var postfix = (Enum.GetName(format)?.ToLower()) ?? throw new NullReferenceException();

        return $"{guid}.{postfix}";
    }
}