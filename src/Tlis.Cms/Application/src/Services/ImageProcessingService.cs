using System;
using System.Drawing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Tlis.Cms.Application.Configurations;
using Tlis.Cms.Application.Services.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;
using Tlis.Cms.Domain.Entities.Images;

namespace Tlis.Cms.Application.Services;

internal sealed class ImageProcessingService(
    IStorageService storageService,
    IImageService imageService)
    : IImageProcessingService
{
    public async Task<Image> CreateImageAsync(IFormFile image, ImageFormatConfiguration format)
    {
        using var originalImageStream = image.OpenReadStream();
        var originalImageId = Guid.NewGuid();
        var cropImageId = Guid.NewGuid();

        var (
            originalImage,
            originalFileSize,
            originalImageSize,
            originalImageWebpUrl
        ) = await ProcessOriginalImageAsync(image, originalImageId);

        using (originalImage)
        {
            var (
                croppedFileSize,
                croppedWebpUrl
            ) = await CropImageAsync(originalImage, originalImageSize, format, cropImageId);

            return new Image
            {
                Id = originalImageId,
                Width = originalImageSize.Width,
                Height = originalImageSize.Height,
                Url = originalImageWebpUrl,
                Size = originalFileSize,
                Crops = [
                    new Crop
                    {
                        Id = cropImageId,
                        Height = format.Height,
                        Width = format.Width,
                        Size = croppedFileSize,
                        Url = croppedWebpUrl
                    }
                ]
            };
        }
    }

    public async Task<(NetVips.Image image, long fileSize, Size imageSize, string url)> ProcessOriginalImageAsync(IFormFile imageFile, Guid imageId)
    {
        using var imageStream = imageFile.OpenReadStream();

        var image = imageService.ToImage(imageStream);
        var imageSize = imageService.GetSize(image);
        using var webp = imageService.ToWebp(image);
        var webpUrl = await storageService.UploadImage(webp, imageId);

        return new (image, webp.Length, imageSize, webpUrl);
    }

    public async Task<(long fileSize, string url)> CropImageAsync(NetVips.Image originalImage, Size originalImageSize, ImageFormatConfiguration configuration, Guid imageId)
    {
        using var croppedImage = imageService.Crop(
            originalImage,
            default,
            default,
            originalImageSize.Width,
            originalImageSize.Height);

        using var resizedCroppedImage = imageService.Resize(
            croppedImage,
            configuration.Width,
            configuration.Height);

        using var webp = imageService.ToWebp(resizedCroppedImage);
        
        var webpUrl = await storageService.UploadImage(webp, imageId);

        return (webp.Length, webpUrl);
    }
}