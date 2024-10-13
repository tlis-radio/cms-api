using System;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Tlis.Cms.Application.Configurations;
using Tlis.Cms.Application.Services.Interfaces;
using Tlis.Cms.Infrastructure.Services.Interfaces;
using Tlis.Cms.Domain.Entities.Images;
using Tlis.Cms.Domain.Constants;

namespace Tlis.Cms.Application.Services;

internal sealed class ImageProcessingService(
    ICloudeStorageService storageService,
    IImageService imageService)
    : IImageProcessingService
{
    public async Task<Image> ProcessImageAsync(IFormFile image, ImageType imageType, ImageFormatConfiguration format)
    {
        var originalImageId = Guid.NewGuid();
        var cropImageId = Guid.NewGuid();

        var (originalImage, originalFileSize, originalImageSize) = ProcessOriginalImage(image, originalImageId);

        using (originalImage)
        {
            using var originalImageStream = image.OpenReadStream();
            using var croppedImageStream = CropImage(originalImage, originalImageSize, format, cropImageId);

            string originalWebpUrl = string.Empty;
            string croppedWebpUrl = string.Empty;
            switch (imageType)
            {
                case ImageType.User:
                    originalWebpUrl = await storageService.UploadUserImageAsync(originalImageStream, originalImageId);
                    croppedWebpUrl = await storageService.UploadUserImageAsync(croppedImageStream, cropImageId);
                    break;
                case ImageType.Show:
                    originalWebpUrl = await storageService.UploadShowImageAsync(originalImageStream, originalImageId);
                    croppedWebpUrl = await storageService.UploadShowImageAsync(croppedImageStream, cropImageId);
                    break;
                case ImageType.Broadcast:
                    originalWebpUrl = await storageService.UploadBroadcastImageAsync(originalImageStream, originalImageId);
                    croppedWebpUrl = await storageService.UploadBroadcastImageAsync(croppedImageStream, cropImageId);
                    break;
                default:
                    throw new NotImplementedException(nameof(imageType));
            }

            return new Image
            {
                Id = originalImageId,
                Width = originalImageSize.Width,
                Height = originalImageSize.Height,
                FileName = originalWebpUrl,
                Size = originalFileSize,
                Crops = [
                    new Crop
                    {
                        Id = cropImageId,
                        Height = format.Height,
                        Width = format.Width,
                        Size = croppedImageStream.Length,
                        FileName = croppedWebpUrl
                    }
                ]
            };
        }
    }

    private (NetVips.Image image, long fileSize, Size imageSize) ProcessOriginalImage(IFormFile imageFile, Guid imageId)
    {
        using var imageStream = imageFile.OpenReadStream();

        var image = imageService.ToImage(imageStream);
        var imageSize = imageService.GetSize(image);
        using var webp = imageService.ToWebp(image);

        return new (image, webp.Length, imageSize);
    }

    private Stream CropImage(NetVips.Image originalImage, Size originalImageSize, ImageFormatConfiguration configuration, Guid imageId)
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

        return imageService.ToWebp(resizedCroppedImage);
    }
}