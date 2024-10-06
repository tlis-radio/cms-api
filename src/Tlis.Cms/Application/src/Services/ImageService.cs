using System.Drawing;
using System.IO;
using NetVips;
using Tlis.Cms.Application.Services.Interfaces;

namespace Tlis.Cms.Application.Services;

internal sealed class ImageService : IImageService
{
    public Stream ToWebp(Image image)
    {
        var result = new MemoryStream();

        image.WebpsaveStream(result, lossless: false);

        return result;
    }

    public Image ToImage(Stream stream)
    {
        return Image.NewFromStream(stream);
    }

    public Image Resize(Image image, int width, int height)
    {
        return image.ThumbnailImage(width, height, crop: Enums.Interesting.Centre);
    }

    public Image Crop(Image image, int left, int top, int width, int height)
    {
        return image.Crop(left, top, width, height);
    }

    public Size GetSize(Image image)
    {
        return new Size
        {
            Height = image.Height,
            Width = image.Width
        };
    }
}